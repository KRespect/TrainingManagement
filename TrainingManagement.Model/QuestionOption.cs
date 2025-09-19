
using SqlSugar;

namespace TrainingManagement.Model
{

    public class QuestionOption
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 是否是正确答案
        /// </summary>
        public bool IsTrue { get; set; }
        public string Key { get; set; } // A, B, C, D
        public string Content { get; set; }
        /// <summary>
        /// 问题Id
        /// </summary>
        public string QuestionId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
