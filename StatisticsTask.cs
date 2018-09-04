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
            sequence.Sort((a, b) =>
            {
                var c1 = a.UserId.CompareTo(b.UserId);
                var c2 = a.DateTime.CompareTo(b.DateTime);
                return (0 != c1) ? c1 : c2;
            });
            try
            {
                return sequence.Bigrams()
                               .Where(t => t.Item1.UserId == t.Item2.UserId &&
                                           t.Item1.SlideId != t.Item2.SlideId &&
                                           t.Item1.SlideType == slideType)
                               .Select(t => t.Item2.DateTime.Subtract(t.Item1.DateTime)
                                                   .TotalMinutes)
                               .Where(d => (1.0 <= d && d <= 120.0))
                               .Median();
            }
            catch (InvalidOperationException)
            { // when no bigrams found;
                return 0.0;
            }
        }
    }
}