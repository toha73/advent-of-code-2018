using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent_of_Code_2018
{
    public class Day4
    {
        public List<(DateTime Date, string Note)> _events = null;
        private Dictionary<int, Guard> _guards = new Dictionary<int, Guard>();

        public int Part1(string input)
        {
            ParseInput(input);

            ParseEvents();

            var mostSleepingGuard = _guards.Values.OrderByDescending(g => g.TotalNapTimeInMinutes).First();

            return mostSleepingGuard.Id * mostSleepingGuard.GetMostLikelySleepingMinute();
        }


        public int Part2(string input)
        {
            ParseInput(input);

            ParseEvents();

            var mostSleepyMinute = _guards.Values
                .SelectMany(g => g.SleepingMinutes
                    .Select(m => new { Guard = g, Minute = m })
                    .GroupBy(x => x.Minute)
                    .Select(x => new { Guard = g, Minute = x.Key, SleepCount = x.Count() }))
                .OrderByDescending(x => x.SleepCount)
                .First();

            return mostSleepyMinute.Guard.Id * mostSleepyMinute.Minute;
        }

        private void ParseEvents()
        {
            Guard currentGuard = null;
            Shift currentShift = null;
            NapTime currentNapTime = null;
            foreach (var ev in _events)
            {
                if (ev.Note.StartsWith("Guard"))
                {
                    if (currentNapTime != null)
                    {
                        currentNapTime.To = ev.Date;
                    }
                    if (currentShift != null)
                    {
                        currentShift.End = ev.Date;
                    }
                    var guardId = int.Parse(Regex.Match(ev.Note, ".* #(\\d*)").Groups[1].Value);
                    if (!_guards.TryGetValue(guardId, out currentGuard))
                    {
                        currentGuard = new Guard { Id = guardId };
                        _guards[guardId] = currentGuard;
                    }
                    currentShift = new Shift { Start = ev.Date };
                    currentGuard.Shifts.Add(currentShift);
                }
                else if (ev.Note == "falls asleep" && currentNapTime == null)
                {
                    currentNapTime = new NapTime { From = ev.Date };
                    currentShift.NapTimes.Add(currentNapTime);
                }
                else if (ev.Note == "wakes up" && currentNapTime != null)
                {
                    currentNapTime.To = ev.Date;
                    currentNapTime = null;
                }
            }
        }

        private void ParseInput(string input)
        {
            _events = input.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(a => a.TrimStart('[').Split(']'))
                .Select(x => (DateTime.Parse(x[0]), x[1].TrimStart()))
                .OrderBy(e => e.Item1)
                .ToList();
        }



        public class Guard
        {
            public int Id { get; set; }
            public List<Shift> Shifts { get; set; } = new List<Shift>();
            public int TotalNapTimeInMinutes => Shifts.Sum(s => s.TotalNapTimeInMinutes);
            public IEnumerable<int> SleepingMinutes => Shifts.SelectMany(n => n.SleepingMinutes);
            public int GetMostLikelySleepingMinute()
            {
                return Shifts
                    .SelectMany(s => s.NapTimes)
                    .SelectMany(n => n.SleepingMinutes)
                    .GroupBy(m => m)
                    .OrderByDescending(g => g.Count())
                    .First().Key;
            }
        }
        public class Shift
        {
            public DateTime Start { get; set; }
            public DateTime? End { get; set; }
            public List<NapTime> NapTimes { get; set; } = new List<NapTime>();
            public int TotalNapTimeInMinutes => NapTimes.Sum(n => n.NapTimeInMinutes);
            public IEnumerable<int> SleepingMinutes => NapTimes.SelectMany(n => n.SleepingMinutes);
        }

        public class NapTime
        {
            public DateTime From { get; set; }
            public DateTime To { get; set; }
            public int NapTimeInMinutes => (int)(To - From).TotalMinutes;
            public IEnumerable<int> SleepingMinutes => Enumerable.Range(From.Minute, NapTimeInMinutes);
        }
    }
}
