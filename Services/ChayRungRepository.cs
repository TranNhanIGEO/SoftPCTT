using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class ChayRungRepository : BaseRepository{
    public ChayRungRepository(IDbConnection connection) : base(connection){}

    public IEnumerable<ChayRung> GetChayRungs(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<ChayRung>("SELECT a.objectid, a.idchay, a.ngay, a.diadiem, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.tgchay, a.tgdap, a.dtchay, a.hientrang,CONCAT(a.maxa, ' - ',xa.tenxa)::character varying AS maxa, CONCAT(a.mahuyen, ' - ', xa.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM ChayRung a LEFT JOIN RgXa xa ON xa.maxa = a.maxa WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen ORDER BY a.ngay ASC"
                , new{
                    _mahuyen = mahuyen
                });
            }
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<ChayRung>("SELECT * FROM GetChayRungs(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<ChayRung>("SELECT a.objectid, a.idchay, a.ngay, a.diadiem, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.tgchay, a.tgdap, a.dtchay, a.hientrang,CONCAT(a.maxa, ' - ',xa.tenxa)::character varying AS maxa, CONCAT(a.mahuyen, ' - ', xa.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM ChayRung a LEFT JOIN RgXa xa ON xa.maxa = a.maxa WHERE " + SqlQuery + " ORDER BY a.ngay ASC");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<ChayRung>("SELECT * FROM GetChayRungs(@_mahuyen)"
            , new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public ChayRung? GetChayRung(int? objectid){
        return connection.QueryFirstOrDefault<ChayRung>("SELECT * FROM GetChayRung(@_objectid)", new{
            _objectid = objectid
        }, commandType: CommandType.Text);
    }
    public int? GetMaxObjectId(){
        if (connection.QueryFirstOrDefault<int?>("SELECT MAX(ObjectId) FROM ChayRung", commandType: CommandType.Text) == null){
            return 0;
        }
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM ChayRung", commandType: CommandType.Text);
    }
    public int Add(ChayRung obj){ 
        double? nulltoado = null;
        string? nullstring = null!;
        double? dtchay = obj.dtchay == null ? null : Convert.ToDouble(obj.dtchay);
        double? toadox = obj.toadox == null ? null : Convert.ToDouble(obj.toadox);
        double? toadoy = obj.toadoy == null ? null : Convert.ToDouble(obj.toadoy);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);

        if (obj.toadox != null && obj.toadoy != null){
            return connection.ExecuteScalar<int>("AddChayRung", 
                new{
                    _objectid = obj.objectid,
                    _idchay = "CR" +  obj.objectid.ToString(),
                    _ngay = obj.ngay,
                    _diadiem = obj.diadiem,
                    _toadox = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadox), 3)),
                    _toadoy = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadoy), 3)),
                    _tgchay = obj.tgchay,
                    _tgdap = obj.tgdap,
                    _dtchay = dtchay,
                    _hientrang = obj.hientrang,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring
                }, commandType: CommandType.StoredProcedure
            );
        }
        return connection.ExecuteScalar<int>("AddChayRung", 
            new{
                    _objectid = obj.objectid,
                    _idchay = "CR" +  obj.objectid.ToString(),
                    _ngay = obj.ngay,
                    _diadiem = obj.diadiem,
                    _toadox = nulltoado,
                    _toadoy = nulltoado,
                    _tgchay = obj.tgchay,
                    _tgdap = obj.tgdap,
                    _dtchay = dtchay,
                    _hientrang = obj.hientrang,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Edit(int objectid, ChayRung obj){ 
        double? nulltoado = null;
        string? nullstring = null!;
        double? dtchay = obj.dtchay == null ? null : Convert.ToDouble(obj.dtchay);
        double? toadox = obj.toadox == null ? null : Convert.ToDouble(obj.toadox);
        double? toadoy = obj.toadoy == null ? null : Convert.ToDouble(obj.toadoy);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);

        if (obj.toadox != null && obj.toadoy != null){
            return connection.ExecuteScalar<int>("EditChayRung", 
                new{
                    _objectid = objectid,
                    _ngay = obj.ngay,
                    _diadiem = obj.diadiem,
                    _toadox = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadox), 3)),
                    _toadoy = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadoy), 3)),
                    _tgchay = obj.tgchay,
                    _tgdap = obj.tgdap,
                    _dtchay = dtchay,
                    _hientrang = obj.hientrang,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring
                }, commandType: CommandType.StoredProcedure
            );
        }
        return connection.ExecuteScalar<int>("EditChayRung", 
            new{
                    _objectid = objectid,
                    _ngay = obj.ngay,
                    _diadiem = obj.diadiem,
                    _toadox = nulltoado,
                    _toadoy = nulltoado,
                    _tgchay = obj.tgchay,
                    _tgdap = obj.tgdap,
                    _dtchay = dtchay,
                    _hientrang = obj.hientrang,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeleteChayRung", new{
            _objectid = objectid
        });
    }
}