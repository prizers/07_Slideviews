using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public static class StringExt
    {
        public static string[] SplitBySemicolon(this string s) => s.Split(';');

        public static SlideRecord ToSlideRecord(this string s)
        {
            var v = s.SplitBySemicolon();
            int slideId;
            SlideType slideType;
            if (3 == v.Count() &&
                int.TryParse(v[0], out slideId) &&
                Enum.TryParse(v[1], true, out slideType))
                return new SlideRecord(slideId, slideType, v[2]);
            else return null;
        }

        public static VisitRecord ToVisitRecord(this string s,
            IDictionary<int, SlideRecord> slides)
        {
            var v = s.SplitBySemicolon();
            int userId;
            int slideId;
            DateTime dateTime;
            if (4 == v.Count() &&
                int.TryParse(v[0], out userId) &&
                int.TryParse(v[1], out slideId) &&
                DateTime.TryParse(v[2] + 'T' + v[3], out dateTime) &&
                slides.ContainsKey(slideId))
                return new VisitRecord(userId, slideId, dateTime, slides[slideId].SlideType);
            else throw new FormatException("Wrong line [" + s + "]");
        }
    }

    public class ParsingTask
    {
        public static IDictionary<int, SlideRecord> ParseSlideRecords(IEnumerable<string> lines)
        {
            return lines.Skip(1)
                        .Select(s => s.ToSlideRecord())
                        .Where(r => (r != null))
                        .ToDictionary(r => r.SlideId, r => r);
        }

        public static IEnumerable<VisitRecord> ParseVisitRecords(
            IEnumerable<string> lines, IDictionary<int, SlideRecord> slides)
        {
            return lines.Skip(1)
                        .Select(s => s.ToVisitRecord(slides));
        }
    }
}