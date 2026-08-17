Imports System.IO

''' <summary>
''' Safe helpers for decoding Nomenclature.Photo bytes into a displayable
''' Image. Centralized because corrupt/foreign image data must never crash
''' the app — both FormNomenclature and FormApercu load photos this way.
''' </summary>
Public Module PhotoHelper

    ''' <summary>
    ''' Decodes the given bytes into a standalone Bitmap (detached from the
    ''' source stream). Returns Nothing if bytes is Nothing/empty or isn't
    ''' valid image data.
    ''' </summary>
    Public Function TryLoadImage(bytes As Byte()) As Image
        If bytes Is Nothing OrElse bytes.Length = 0 Then Return Nothing

        Try
            Using ms As New MemoryStream(bytes)
                Using loaded = Image.FromStream(ms)
                    Return New Bitmap(loaded)
                End Using
            End Using
        Catch ex As Exception When TypeOf ex Is ArgumentException OrElse TypeOf ex Is OutOfMemoryException
            ' Not valid image data (corrupt file, unsupported format, etc).
            Return Nothing
        End Try
    End Function

End Module
