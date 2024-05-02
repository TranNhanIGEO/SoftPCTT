using System.Data;
using WebApi.Models;
using Dapper;
namespace WebApi.Services;

public class DanhBaDTRepository : BaseRepository{
    public DanhBaDTRepository(IDbConnection connection) : base(connection){}

    public IEnumerable<DanhBaDT> GetDanhBaDTs(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<DanhBaDT>("SELECT a.objectid, a.iddanhba, a.quanhuyen, a.hoten, a.cvcoquan, a.cvbch, a.dtcoquan, a.dtdidong, a.fax, a.mahuyen, a.namcapnhat, a.ghichu FROM DanhBaDT a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen ORDER BY a.quanhuyen ASC"
                , new{
                    _mahuyen = mahuyen
                });
            } 
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<DanhBaDT>("SELECT * FROM GetDanhBaDTs(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<DanhBaDT>("SELECT a.objectid, a.iddanhba, a.quanhuyen, a.hoten, a.cvcoquan, a.cvbch, a.dtcoquan, a.dtdidong, a.fax, a.mahuyen, a.namcapnhat, a.ghichu FROM DanhBaDT a WHERE " + SqlQuery + " ORDER BY a.quanhuyen ASC");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<DanhBaDT>("SELECT * FROM GetDanhBaDTs(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public DanhBaDTAddEdit? GetDanhBaDT(int objectid){
        return connection.QueryFirstOrDefault<DanhBaDTAddEdit>("SELECT * FROM GetDanhBaDT(@_objectid)", new{
            _objectid = objectid
        });
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM DanhBaDT", commandType: CommandType.Text);
    }
    public int Add(DanhBaDTAddEdit obj){ 
        int? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt32(obj.namcapnhat);

        return connection.ExecuteScalar<int>("AddDanhBaDT", 
            new{
                _objectid = obj.objectid,
                _iddanhba = "DBDT" + obj.objectid.ToString(),
                _quanhuyen = obj.quanhuyen,
                _hoten = obj.hoten,
                _cvcoquan = obj.cvcoquan,
                _cvbch = obj.cvbch,
                _dtcoquan = obj.dtcoquan,
                _dtdidong = obj.dtdidong,
                _fax = obj.fax,
                _mahuyen = obj.mahuyen,
                _namcapnhat = namcapnhat
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Edit(int objectid, DanhBaDTAddEdit obj){ 
        int? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt32(obj.namcapnhat);

        return connection.ExecuteScalar<int>("EditDanhBaDT", 
            new{
                _objectid = objectid,
                _quanhuyen = obj.quanhuyen,
                _hoten = obj.hoten,
                _cvcoquan = obj.cvcoquan,
                _cvbch = obj.cvbch,
                _dtcoquan = obj.dtcoquan,
                _dtdidong = obj.dtdidong,
                _fax = obj.fax,
                _mahuyen = obj.mahuyen,
                _namcapnhat = namcapnhat
            }, commandType: CommandType.StoredProcedure
        );        
    }
    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeleteDanhbaDT", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
}