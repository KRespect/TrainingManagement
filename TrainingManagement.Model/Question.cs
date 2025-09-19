
using Oracle.ManagedDataAccess.Types;
using SqlSugar;
using TrainingManagement.Model;

namespace TrainingManagement.Model
{

    [SugarTable("questions")]
    public class Question
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 问题类型
        /// </summary>
        public int Type { get; set; } // 1:单选题, 2:多选题, 3:简答题
       /// <summary>
       /// 问题难度
       /// </summary>
        public int Difficulty { get; set; } // 1:简单, 2:一般, 3:困难

        [SugarColumn(IsJson = true)]
        public List<QuestionOption> Options { get; set; }
        /// <summary>
        ///  多选题可能有多个正确答案
        /// </summary>
        [SugarColumn(IsJson = true)]
        public List<string> CorrectAnswers { get; set; }
        /// <summary>
        /// 分值
        /// </summary>
        public decimal Score { get; set; }
       /// <summary>
       /// 创建人ID
       /// </summary>
        public int CreatorId { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(CreatorId))]
        public User Creator { get; set; }

        public DateTime CreateTime { get; set; } = DateTime.Now;
        [Navigate(NavigateType.OneToMany, nameof(QuestionOption.QuestionId))]
        public List<QuestionOption> QuestionOptions { get; set; }

    }

}
