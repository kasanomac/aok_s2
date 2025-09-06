namespace aok_s2.Models;
public class ClassMajor
{
    public int ClassId { get; set; }
    public Class Class { get; set; }

    public int MajorId { get; set; }
    public Major Major { get; set; }
}