Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports Facebook
Imports Facebook.Web

Imports Newtonsoft.Json.Linq

Partial Class test
    Inherits System.Web.UI.Page

   

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
       
    End Sub



    





    Protected Sub CheckEXIF(file_path As String)


        '/// "Exif IFD"
        '/// "Gps IFD"
        '/// "New Subfile Type"
        '/// "Subfile Type"
        '/// "Image Width"
        '/// "Image Height"
        '/// "Bits Per Sample"
        '/// "Compression"
        '/// "Photometric Interp"
        '/// "Thresh Holding"
        '/// "Cell Width"
        '/// "Cell Height"
        '/// "Fill Order"
        '/// "Document Name"
        '/// "Image Description"
        '/// "Equip Make"
        '/// "Equip Model"
        '/// "Strip Offsets"
        '/// "Orientation"
        '/// "Samples PerPixel"
        '/// "Rows Per Strip"
        '/// "Strip Bytes Count"
        '/// "Min Sample Value"
        '/// "Max Sample Value"
        '/// "X Resolution"
        '/// "Y Resolution"
        '/// "Planar Config"
        '/// "Page Name"
        '/// "X Position"
        '/// "Y Position"
        '/// "Free Offset"
        '/// "Free Byte Counts"
        '/// "Gray Response Unit"
        '/// "Gray Response Curve"
        '/// "T4 Option"
        '/// "T6 Option"
        '/// "Resolution Unit"
        '/// "Page Number"
        '/// "Transfer Funcition"
        '/// "Software Used"
        '/// "Date Time"
        '/// "Artist"
        '/// "Host Computer"
        '/// "Predictor"
        '/// "White Point"
        '/// "Primary Chromaticities"
        '/// "ColorMap"
        '/// "Halftone Hints"
        '/// "Tile Width"
        '/// "Tile Length"
        '/// "Tile Offset"
        '/// "Tile ByteCounts"
        '/// "InkSet"
        '/// "Ink Names"
        '/// "Number Of Inks"
        '/// "Dot Range"
        '/// "Target Printer"
        '/// "Extra Samples"
        '/// "Sample Format"
        '/// "S Min Sample Value"
        '/// "S Max Sample Value"
        '/// "Transfer Range"
        '/// "JPEG Proc"
        '/// "JPEG InterFormat"
        '/// "JPEG InterLength"
        '/// "JPEG RestartInterval"
        '/// "JPEG LosslessPredictors"
        '/// "JPEG PointTransforms"
        '/// "JPEG QTables"
        '/// "JPEG DCTables"
        '/// "JPEG ACTables"
        '/// "YCbCr Coefficients"
        '/// "YCbCr Subsampling"
        '/// "YCbCr Positioning"
        '/// "REF Black White"
        '/// "ICC Profile"
        '/// "Gamma"
        '/// "ICC Profile Descriptor"
        '/// "SRGB RenderingIntent"
        '/// "Image Title"
        '/// "Copyright"
        '/// "Resolution X Unit"
        '/// "Resolution Y Unit"
        '/// "Resolution X LengthUnit"
        '/// "Resolution Y LengthUnit"
        '/// "Print Flags"
        '/// "Print Flags Version"
        '/// "Print Flags Crop"
        '/// "Print Flags Bleed Width"
        '/// "Print Flags Bleed Width Scale"
        '/// "Halftone LPI"
        '/// "Halftone LPIUnit"
        '/// "Halftone Degree"
        '/// "Halftone Shape"
        '/// "Halftone Misc"
        '/// "Halftone Screen"
        '/// "JPEG Quality"
        '/// "Grid Size"
        '/// "Thumbnail Format"
        '/// "Thumbnail Width"
        '/// "Thumbnail Height"
        '/// "Thumbnail ColorDepth"
        '/// "Thumbnail Planes"
        '/// "Thumbnail RawBytes"
        '/// "Thumbnail Size"
        '/// "Thumbnail CompressedSize"
        '/// "Color Transfer Function"
        '/// "Thumbnail Data"
        '/// "Thumbnail ImageWidth"
        '/// "Thumbnail ImageHeight"
        '/// "Thumbnail BitsPerSample"
        '/// "Thumbnail Compression"
        '/// "Thumbnail PhotometricInterp"
        '/// "Thumbnail ImageDescription"
        '/// "Thumbnail EquipMake"
        '/// "Thumbnail EquipModel"
        '/// "Thumbnail StripOffsets"
        '/// "Thumbnail Orientation"
        '/// "Thumbnail SamplesPerPixel"
        '/// "Thumbnail RowsPerStrip"
        '/// "Thumbnail StripBytesCount"
        '/// "Thumbnail ResolutionX"
        '/// "Thumbnail ResolutionY"
        '/// "Thumbnail PlanarConfig"
        '/// "Thumbnail ResolutionUnit"
        '/// "Thumbnail TransferFunction"
        '/// "Thumbnail SoftwareUsed"
        '/// "Thumbnail DateTime"
        '/// "Thumbnail Artist"
        '/// "Thumbnail WhitePoint"
        '/// "Thumbnail PrimaryChromaticities"
        '/// "Thumbnail YCbCrCoefficients"
        '/// "Thumbnail YCbCrSubsampling"
        '/// "Thumbnail YCbCrPositioning"
        '/// "Thumbnail RefBlackWhite"
        '/// "Thumbnail CopyRight"
        '/// "Luminance Table"
        '/// "Chrominance Table"
        '/// "Frame Delay"
        '/// "Loop Count"
        '/// "Pixel Unit"
        '/// "Pixel PerUnit X"
        '/// "Pixel PerUnit Y"
        '/// "Palette Histogram"
        '/// "Exposure Time"
        '/// "F-Number"
        '/// "Exposure Prog"
        '/// "Spectral Sense"
        '/// "ISO Speed"
        '/// "OECF"
        '/// "Ver"
        '/// "DTOrig"
        '/// "DTDigitized"
        '/// "CompConfig"
        '/// "CompBPP"
        '/// "Shutter Speed"
        '/// "Aperture"
        '/// "Brightness"
        '/// "Exposure Bias"
        '/// "MaxAperture"
        '/// "SubjectDist"
        '/// "Metering Mode"
        '/// "LightSource"
        '/// "Flash"
        '/// "FocalLength"
        '/// "Maker Note"
        '/// "User Comment"
        '/// "DTSubsec"
        '/// "DTOrigSS"
        '/// "DTDigSS"
        '/// "FPXVer"
        '/// "ColorSpace"
        '/// "PixXDim"
        '/// "PixYDim"
        '/// "RelatedWav"
        '/// "Interop"
        '/// "FlashEnergy"
        '/// "SpatialFR"
        '/// "FocalXRes"
        '/// "FocalYRes"
        '/// "FocalResUnit"
        '/// "Subject Loc"
        '/// "Exposure Index"
        '/// "Sensing Method"
        '/// "FileSource"
        '/// "SceneType"

        Dim bmp As System.Drawing.Bitmap = New System.Drawing.Bitmap(file_path)
        Dim er As Goheer.EXIF.EXIFextractor = New Goheer.EXIF.EXIFextractor(bmp, "\n")
        'MsgBox(er.Item("ISO Speed"))



    End Sub



















End Class
