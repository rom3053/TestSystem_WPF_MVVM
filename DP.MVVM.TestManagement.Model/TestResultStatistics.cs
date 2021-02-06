using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.MVVM.TestManagement.Model
{
    public class TestResultStatistics
    {
        public int StatsID { get; set; }
        public int UserID { get; set; }
        public int TestID { get; set; }

        public string StatsTestName { get; set; }
        public string StatsTestCategory { get; set; }
        public double StatsResultTest { get; set; }
        public List<Question> StatsQuestion { get; set; }
        public Dictionary<int, int[]> StatsSelectedAnswers { get; set; }
        public string StatsTestCompletionDate { get; set; }

        public string UserName { get; set; }
        public string UserLogin { get; set; }
        public string GroupName { get; set; }
    }
}
