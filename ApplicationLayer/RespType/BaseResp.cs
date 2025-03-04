namespace Application.RespType;

public class BaseResp
{
  public int Code { get; set; }
  public string Message { get; set; } = null!;
}

public class GenericResp<T> : BaseResp
{
  public T? Data { get; set; }
}

public class DynamicResponse<T> : BaseResp
{
    public MegaData<T> Data { get; set; }

}

public class MegaData<T>
{
    public PagingMetaData PageInfo { get; set; }
    public SearchCondition SearchInfo { get; set; }
    public List<T> PageData { get; set; }
}

public class PagingMetaData
{
    public int Page { get; set; }
    public int Size { get; set; }
    public string? Sort { get; set; }
    public string? Order { get; set; }
    public int TotalPage { get; set; }
    public int TotalItem { get; set; }
}

public class PagingModel
{
    public int Page { get; set; }
    public int Size { get; set; }
    public string? Sort { get; set; }
    public string? Order { get; set; }
}
public class SearchCondition
{
    public string? keyWord { get; set; }
    public string? role { get; set; }
    public bool? status { get; set; }
    public bool? is_Verify { get; set; }
    public bool? is_Delete { get; set; }
}

