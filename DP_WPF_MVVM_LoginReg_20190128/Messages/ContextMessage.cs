using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_WPF_MVVM_LoginReg_20190128.Messages
{
    public enum StatesContextMessage
    {
        SendBackGroups,
        SendBackCategoryCancel,
        SendCategoryEdit,
        SendTestStatistic,
        SendTestEdit,
        UpdateCategoryInCategoryBrwoser,
        UpdateCategoryInCreateTest
    }
    public class ContextMessage
    {
        public static StatesContextMessage SendBackGroupsToCreateTestViewModel = StatesContextMessage.SendBackGroups;
        public static StatesContextMessage SendBackCategoryCancel = StatesContextMessage.SendBackCategoryCancel;
        public static StatesContextMessage SendCategoryEdit = StatesContextMessage.SendCategoryEdit;
        public static StatesContextMessage SendTestStatistic = StatesContextMessage.SendTestStatistic;
        public static StatesContextMessage SendTestEdit = StatesContextMessage.SendTestEdit;
        public static StatesContextMessage UpdateCategoryInCategoryBrwoser = StatesContextMessage.UpdateCategoryInCategoryBrwoser;
        public static StatesContextMessage UpdateCategoryInCreateTest = StatesContextMessage.UpdateCategoryInCreateTest;
    }
}
