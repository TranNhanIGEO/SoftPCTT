using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class HuongDiChuyenRepository : BaseRepository{
    public HuongDiChuyenRepository(IDbConnection connection) : base(connection){}

    public IEnumerable<HuongDiChuyen> GetHuongDiChuyens(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<HuongDiChuyen>("SELECT a.objectid, a.iddichuyen, a.chieudai::Numeric, a.tenhuong, a.khuvuc, a.namcapnhat, a.ghichu, a.mahuyen FROM HuongDiChuyen a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen ORDER BY a.khuvuc ASC"
                ,new {
                    _mahuyen = mahuyen
                });
            }   
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<HuongDiChuyen>("SELECT * FROM GetHuongDiChuyens(@_mahuyen)", new{
                _mahuyen = mahuyen
            });       
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){   
                return connection.Query<HuongDiChuyen>("SELECT a.objectid, a.iddichuyen, a.chieudai::Numeric, a.tenhuong, a.khuvuc, a.namcapnhat, a.ghichu, a.mahuyen FROM HuongDiChuyen a WHERE " + SqlQuery + " ORDER BY a.khuvuc ASC");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<HuongDiChuyen>("SELECT * FROM GetHuongDiChuyens(@_mahuyen)", new{
                _mahuyen = mahuyen
            });
        }        
    }
    public HuongDiChuyen? GetHuongDiChuyen(int objectid){
        return connection.QueryFirstOrDefault<HuongDiChuyen>("SELECT * FROM GetHuongDiChuyen(@_objectid)", new{
            _objectid = objectid
        });
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM HuongDiChuyen", commandType: CommandType.Text);
    }
    public int Add(HuongDiChuyen obj){ 
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        double? chieudai = obj.chieudai == null ? null : Convert.ToDouble(obj.chieudai);

        return connection.ExecuteScalar<int>("AddHuongDiChuyen", 
            new{
                _objectid = Convert.ToInt32(obj.objectid),
                _chieudai = chieudai,
                _tenhuong = Convert.ToString(obj.tenhuong),
                _khuvuc = Convert.ToString(obj.khuvuc),
                _namcapnhat = namcapnhat,
                // _ghichu = Convert.ToString(obj.ghichu),
                _mahuyen = Convert.ToString(obj.mahuyen)
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Edit(int objectid, HuongDiChuyen obj){ 
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        double? chieudai = obj.chieudai == null ? null : Convert.ToDouble(obj.chieudai);

        return connection.ExecuteScalar<int>("EditHuongDiChuyen", 
            new{
                _objectid = objectid,
                _chieudai = chieudai,
                _tenhuong = Convert.ToString(obj.tenhuong),
                _khuvuc = Convert.ToString(obj.khuvuc),
                _namcapnhat = namcapnhat,
                // _ghichu = Convert.ToString(obj.ghichu),
                _mahuyen = Convert.ToString(obj.mahuyen)
            }, commandType: CommandType.StoredProcedure
        );        
    }
    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeleteHuongDiChuyen", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
}