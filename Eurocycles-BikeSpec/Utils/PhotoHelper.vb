Imports System.Drawing.Imaging
Imports System.IO
Imports System.Linq

''' <summary>
''' Safe helpers for decoding Nomenclature.Photo bytes into a displayable
''' Image. Centralized because corrupt/foreign image data must never crash
''' the app — both FormNomenclature and FormApercu load photos this way.
''' </summary>
Public Module PhotoHelper

    ''' <summary>Upper bound enforced on chosen photo files, checked before the file is
    ''' even read into memory (btnChargerPhoto_Click) so an oversized/decompression-bomb-style
    ''' file can't be used to bloat the database or spike memory decoding it.</summary>
    Public Const MaxPhotoSizeBytes As Long = 5 * 1024 * 1024 ' 5 MB

    ''' <summary>Image formats accepted for upload/display, matching what the file picker's
    ''' filter advertises (PNG/JPEG/BMP). Anything else — even if GDI+ can decode it (GIF,
    ''' TIFF, ICO, WMF/EMF...) — is rejected, so the accepted set is an explicit allow-list
    ''' rather than "whatever the decoder happens to support".</summary>
    Private ReadOnly AllowedFormats As Guid() = {
        ImageFormat.Png.Guid, ImageFormat.Jpeg.Guid, ImageFormat.Bmp.Guid
    }

    ''' <summary>
    ''' Decodes the given bytes into a standalone Bitmap (detached from the
    ''' source stream). Returns Nothing if bytes is Nothing/empty, exceeds
    ''' MaxPhotoSizeBytes, isn't valid image data, or isn't one of AllowedFormats.
    ''' </summary>
    Public Function TryLoadImage(bytes As Byte()) As Image
        If bytes Is Nothing OrElse bytes.Length = 0 Then Return Nothing
        If bytes.LongLength > MaxPhotoSizeBytes Then Return Nothing

        Try
            Using ms As New MemoryStream(bytes)
                Using loaded = Image.FromStream(ms)
                    If Not AllowedFormats.Contains(loaded.RawFormat.Guid) Then Return Nothing
                    Return New Bitmap(loaded)
                End Using
            End Using
        Catch ex As Exception When TypeOf ex Is ArgumentException OrElse TypeOf ex Is OutOfMemoryException
            ' Not valid image data (corrupt file, unsupported format, etc).
            Return Nothing
        End Try
    End Function

End Module
