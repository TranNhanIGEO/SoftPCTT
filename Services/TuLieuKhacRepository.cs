using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class TuLieuKhacRepository : BaseRepository{
    public TuLieuKhacRepository(IDbConnection connection) : base(connection){}

    public IEnumerable<TuLieuKhac> GetTuLieuKhacs(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<TuLieuKhac>("SELECT a.tentulieu, TO_CHAR(a.ngaytulieu, 'dd/mm/yyyy')::Text AS ngaytulieu, a.noidung, a.diadiem, a.dvql, a.nguongoc, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu FROM TuLieuKhac a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen ORDER BY a.ngaytulieu ASC"
                , new{
                    _mahuyen = mahuyen
                });
            }
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<TuLieuKhac>("SELECT * FROM GetTuLieuKhacs(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<TuLieuKhac>("SELECT a.tentulieu, TO_CHAR(a.ngaytulieu, 'dd/mm/yyyy')::Text AS ngaytulieu, a.noidung, a.diadiem, a.dvql, a.nguongoc, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu FROM TuLieuKhac a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " ORDER BY a.ngaytulieu ASC");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<TuLieuKhac>("SELECT * FROM GetTuLieuKhacs(@_mahuyen)"
            , new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public string? GetOldName(int objectid){
        return connection.QueryFirstOrDefault<string>("SELECT tentulieu FROM TuLieuKhac WHERE objectid = @_objectid", new{_objectid = objectid});
    }
    public TuLieuKhacDetail? GetTuLieuKhac(int objectid){
        return connection.QueryFirstOrDefault<TuLieuKhacDetail>("SELECT * FROM GetTuLieuKhac(@_objectid)", new{
            _objectid = objectid
        });
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM TuLieuKhac", commandType: CommandType.Text);
    }
    public int Add(TuLieuKhacAddEdit obj, int objectid, string? tentulieu){
        string? noidung = obj.noidung == "null" ? null : obj.noidung;
        string? diadiem = obj.diadiem == "null" ? null : obj.diadiem;
        string? dvql = obj.dvql == "null" ? null : obj.dvql;
        string? nguongoc = obj.nguongoc == "null" ? null : obj.nguongoc;
        string? maxa = obj.maxa == "null" ? null : obj.maxa;
        string? mahuyen = obj.mahuyen == "null" ? null : obj.mahuyen;
        short? namcapnhat = obj.namcapnhat == "null" ? null : Convert.ToInt16(obj.namcapnhat);

        if (obj.ngaytulieu != "null"){
            DateTime ngaytulieu = Convert.ToDateTime(obj.ngaytulieu);
            string[] date = obj.ngaytulieu!.ToString().Split('-');
            return connection.ExecuteScalar<int>("AddTuLieuKhac", 
                new{
                    _objectid  = objectid,
                    _idtulieu  = "KHAC" +  objectid.ToString(),
                    _tentulieu  = tentulieu,
                    _ngaytulieu = ngaytulieu,
                    _noidung  = noidung,
                    _diadiem  = diadiem,
                    _dvql  = dvql,
                    _nguongoc  = nguongoc,
                    _maxa  = maxa,
                    _mahuyen  = mahuyen,
                    _namcapnhat = namcapnhat,
                    // _ghichu character varying,
                    _day = Convert.ToInt32(date[2]),
                    _month = Convert.ToInt32(date[1]),
                    _year = Convert.ToInt32(date[0])
                }, commandType: CommandType.StoredProcedure
            );
        }
        DateTime? ngaynull = null;
        int? day = null;
        int? month = null;
        int? year = null;        
        return connection.ExecuteScalar<int>("AddTuLieuKhac", 
            new{
                    _objectid  = objectid,
                    _idtulieu  = "KHAC" +  objectid.ToString(),
                    _tentulieu  = tentulieu,
                    _ngaytulieu = ngaynull,
                    _noidung  = noidung,
                    _diadiem  = diadiem,
                    _dvql  = dvql,
                    _nguongoc  = nguongoc,
                    _maxa  = maxa,
                    _mahuyen  = mahuyen,
                    _namcapnhat = namcapnhat,
                    // _ghichu character varying,
                    _day = day,
                    _month = month,
                    _year = year
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Edit(TuLieuKhacAddEdit obj, int objectid, string? tentulieu){
        string? noidung = obj.noidung == "null" ? null : obj.noidung;
        string? diadiem = obj.diadiem == "null" ? null : obj.diadiem;
        string? dvql = obj.dvql == "null" ? null : obj.dvql;
        string? nguongoc = obj.nguongoc == "null" ? null : obj.nguongoc;
        string? maxa = obj.maxa == "null" ? null : obj.maxa;
        string? mahuyen = obj.mahuyen == "null" ? null : obj.mahuyen;
        short? namcapnhat = obj.namcapnhat == "null" ? null : Convert.ToInt16(obj.namcapnhat);

        if (obj.ngaytulieu != "null"){
            DateTime ngaytulieu = Convert.ToDateTime(obj.ngaytulieu);
            string[] date = obj.ngaytulieu!.ToString().Split('-');
            return connection.ExecuteScalar<int>("EditTuLieuKhac", 
                new{
                    _objectid  = objectid,
                    _tentulieu  = tentulieu,
                    _ngaytulieu = ngaytulieu,
                    _noidung  = noidung,
                    _diadiem  = diadiem,
                    _dvql  = dvql,
                    _nguongoc  = nguongoc,
                    _maxa  = maxa,
                    _mahuyen  = mahuyen,
                    _namcapnhat = namcapnhat,
                    // _ghichu character varying,
                    _day = Convert.ToInt32(date[2]),
                    _month = Convert.ToInt32(date[1]),
                    _year = Convert.ToInt32(date[0])
                }, commandType: CommandType.StoredProcedure
            );
        }
        DateTime? ngaynull = null;
        int? day = null;
        int? month = null;
        int? year = null;        
        return connection.ExecuteScalar<int>("EditTuLieuKhac", 
            new{
                    _objectid  = objectid,
                    _tentulieu  = tentulieu,
                    _ngaytulieu = ngaynull,
                    _noidung  = noidung,
                    _diadiem  = diadiem,
                    _dvql  = dvql,
                    _nguongoc  = nguongoc,
                    _maxa  = maxa,
                    _mahuyen  = mahuyen,
                    _namcapnhat = namcapnhat,
                    // _ghichu character varying,
                    _day = day,
                    _month = month,
                    _year = year
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeleteTuLieuKhac", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
}
