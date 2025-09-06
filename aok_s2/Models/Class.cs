namespace aok_s2.Models;
public class Class
{
    public int Id { get; set; }
    public required string Semester { get; set; }
    public required string ClassFormation { get; set; }
    public required string Department { get; set; }
    public required  string ClassName { get; set; }
    public required string Period { get; set; }
    public required string Teacher { get; set; }

    // Navigation property（中間テーブルへの）
    public ICollection<ClassMajor> ClassMajors { get; set; }
}