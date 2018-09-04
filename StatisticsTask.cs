using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public class StatisticsTask
    {
        public static double GetMedianTimePerSlide(List<VisitRecord> visits, SlideType slideType)
        {
            var sequence = visits.ToList();
            if (!visits.Any()) return 0.0; // return 0 if empty list
            sequence.Sort((a, b) =>
            {
                var c1 = a.UserId.CompareTo(b.UserId);
                var c2 = a.DateTime.CompareTo(b.DateTime);
                return (0 != c1) ? c1 : c2;
            });
            var filtered = sequence.Bigrams()
                                   .Where(t => t.Item1.UserId == t.Item2.UserId &&
                                          t.Item1.SlideId != t.Item2.SlideId &&
                                          t.Item1.SlideType == slideType)
                                   .Select(t => t.Item2.DateTime.Subtract(t.Item1.DateTime)
                                                 .TotalMinutes)
                                   .Where(d => (1.0 <= d && d <= 120.0));
            if (!filtered.Any()) return 0.0; // no slide views
            return filtered.Median();
        }
    }
}