using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.Fonts;
using System.IO;
using System.Text.RegularExpressions;
using SixLabors.ImageSharp.Drawing;

[Route("avatar")]
[ApiController]
public class AvatarController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAvatar([FromQuery] string name, int size = 128)
    {
        if (string.IsNullOrWhiteSpace(name)) return BadRequest("Name required");

        string initials = GetInitials(name);
        Color bgColor = GetDarkColorFromInitials(initials);

        using var image = new Image<Rgba32>(size, size);
        image.Mutate(ctx =>
        {
            ctx.Clear(Color.Transparent);

            var center = new PointF(size / 2f, size / 2f);
            float radius = size / 2f;
            var circle = new EllipsePolygon(center, radius);
            ctx.Fill(bgColor, circle);

            var fontFamily = SystemFonts.Collection.Families.First();
            var font = SystemFonts.CreateFont(fontFamily.Name, size / 2f, FontStyle.Bold);

            var textOptions = new RichTextOptions(font)
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Origin = center
            };

            ctx.DrawText(textOptions, initials, Color.White);
        });

        var ms = new MemoryStream();
        image.SaveAsPng(ms);
        ms.Position = 0;
        return File(ms, "image/png");
    }

    private static Color GetDarkColorFromInitials(string initials)
    {
        // Hash initials to get a consistent seed
        int hash = initials.GetHashCode();

        // Generate hue from 0–360 degrees
        float hue = Math.Abs(hash % 360);

        // Saturation 0.6–1.0, Value (brightness) capped to max 0.4 for dark color
        float saturation = 0.6f + (Math.Abs((hash >> 8) % 40) / 100f); // 0.6–1.0
        float value = 0.2f + (Math.Abs((hash >> 16) % 20) / 100f);      // 0.2–0.4

        // Convert HSV to RGB
        return ColorFromHSV(hue, saturation, value);
    }

    private static Color ColorFromHSV(float hue, float saturation, float value)
    {
        int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
        float f = hue / 60 - MathF.Floor(hue / 60);

        value = value * 255;
        byte v = (byte)value;
        byte p = (byte)(value * (1 - saturation));
        byte q = (byte)(value * (1 - f * saturation));
        byte t = (byte)(value * (1 - (1 - f) * saturation));

        return hi switch
        {
            0 => Color.FromRgb(v, t, p),
            1 => Color.FromRgb(q, v, p),
            2 => Color.FromRgb(p, v, t),
            3 => Color.FromRgb(p, q, v),
            4 => Color.FromRgb(t, p, v),
            _ => Color.FromRgb(v, p, q),
        };
    }

    private string GetInitials(string name)
    {
        var words = Regex.Split(name.Trim(), @"\s+");
        var initials = string.Join("", words.Take(2).Select(w => w[0])).ToUpper();
        return initials;
    }
}
