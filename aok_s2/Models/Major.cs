namespace aok_s2.Models;
public class Major
{
    public required int Id { get; set; }
    public required string MajorName { get; set; }

    // 関連するClass（1つの専攻に複数の授業）
    public ICollection<ClassMajor> ClassMajors { get; set; }
}