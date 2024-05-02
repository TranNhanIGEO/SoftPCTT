using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class KeRepository : BaseRepository{
    public KeRepository(IDbConnection connection) : base(connection){}
    public IEnumerable<Ke> GetKes(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
       // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<Ke>("SELECT a.objectid, a.idke, a.tenkenhmuong, a.vitri, a.chieudai::Numeric, a.caotrinhdaykenh, a.berongkenh, a.hesomai, a.caotrinhbotrai, a.caotrinhbophai, a.berongbotrai, a.berongbophai, a.hanhlangbaove, a.capcongtrinh, a.cumcongtrinh, a.ketcaucongtrinh, a.muctieunhiemvu, a.diadiem, a.namsudung, a.hethongcttl, a.donviquanly, a. namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM Ke a LEFT JOIN rghuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326)) WHERE h.MaHuyen = @_mahuyen AND " + SqlQuery +""
                , new{
                    _mahuyen = mahuyen
                });
            }
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<Ke>("SELECT * FROM GetKes(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<Ke>("SELECT a.objectid, a.idke, a.tenkenhmuong, a.vitri, a.chieudai::Numeric, a.caotrinhdaykenh, a.berongkenh, a.hesomai, a.caotrinhbotrai, a.caotrinhbophai, a.berongbotrai, a.berongbophai, a.hanhlangbaove, a.capcongtrinh, a.cumcongtrinh, a.ketcaucongtrinh, a.muctieunhiemvu, a.diadiem, a.namsudung, a.hethongcttl, a.donviquanly, a. namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM Ke a LEFT JOIN rghuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326)) WHERE " + SqlQuery + "");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<Ke>("SELECT * FROM GetKes(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public IEnumerable<EmbankmentStatistics> GetEmbankmentStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<EmbankmentStatistics>("SELECT a.quan_huyen_tp, a.tongsophantu, a.tongchieudaike FROM ( (SELECT h.tenhuyen AS quan_huyen_tp, COUNT(h.tenhuyen) AS tongsophantu, SUM(a.chieudai)::Numeric AS tongchieudaike FROM Ke a LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326)) WHERE h.MaHuyen = @_mahuyen AND " + SqlQuery + " GROUP BY h.tenhuyen) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(h.tenhuyen) AS tongsophantu, SUM(a.chieudai)::Numeric AS tongchieudaike FROM Ke a LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326)) WHERE h.MaHuyen = @_mahuyen AND " + SqlQuery + ")) a ORDER BY CASE WHEN a.quan_huyen_tp LIKE 'Huyện%' THEN 1 WHEN a.quan_huyen_tp LIKE 'Quận%' THEN 2 WHEN a.quan_huyen_tp LIKE 'Thành%' THEN 3 ELSE 4 END ASC"
                , new{
                    _mahuyen = mahuyen
                });
            }
                return connection.Query<EmbankmentStatistics>("SELECT a.quan_huyen_tp, a.tongsophantu, a.tongchieudaike FROM ( (SELECT h.tenhuyen AS quan_huyen_tp, COUNT(h.tenhuyen) AS tongsophantu, SUM(a.chieudai)::Numeric AS tongchieudaike FROM Ke a LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326)) WHERE h.MaHuyen = @_mahuyen GROUP BY h.tenhuyen) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(h.tenhuyen) AS tongsophantu, SUM(a.chieudai)::Numeric AS tongchieudaike FROM Ke a LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326)) WHERE h.MaHuyen = @_mahuyen)) a ORDER BY CASE WHEN a.quan_huyen_tp LIKE 'Huyện%' THEN 1 WHEN a.quan_huyen_tp LIKE 'Quận%' THEN 2 WHEN a.quan_huyen_tp LIKE 'Thành%' THEN 3 ELSE 4 END ASC"
                , new{
                    _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<EmbankmentStatistics>("SELECT a.quan_huyen_tp, a.tongsophantu, a.tongchieudaike FROM ( (SELECT h.tenhuyen AS quan_huyen_tp, COUNT(h.tenhuyen) AS tongsophantu, SUM(a.chieudai)::Numeric AS tongchieudaike FROM Ke a LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326)) WHERE " + SqlQuery + " GROUP BY h.tenhuyen) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(h.tenhuyen) AS tongsophantu, SUM(a.chieudai)::Numeric AS tongchieudaike FROM Ke a LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326)) WHERE " + SqlQuery + ")) a ORDER BY CASE WHEN a.quan_huyen_tp LIKE 'Huyện%' THEN 1 WHEN a.quan_huyen_tp LIKE 'Quận%' THEN 2 WHEN a.quan_huyen_tp LIKE 'Thành%' THEN 3 ELSE 4 END ASC");            
            }
            return connection.Query<EmbankmentStatistics>("SELECT a.quan_huyen_tp, a.tongsophantu, a.tongchieudaike FROM ( (SELECT h.tenhuyen AS quan_huyen_tp, COUNT(h.tenhuyen) AS tongsophantu, SUM(a.chieudai)::Numeric AS tongchieudaike FROM Ke a LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326))  GROUP BY h.tenhuyen) UNION ALL (SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(h.tenhuyen) AS tongsophantu, SUM(a.chieudai)::Numeric AS tongchieudaike FROM Ke a LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326))) ) a ORDER BY CASE WHEN a.quan_huyen_tp LIKE 'Huyện%' THEN 1 WHEN a.quan_huyen_tp LIKE 'Quận%' THEN 2 WHEN a.quan_huyen_tp LIKE 'Thành%' THEN 3 ELSE 4 END ASC");  
        }       
    } 
    public Ke? GetKe(int objectid){
        return connection.QueryFirstOrDefault<Ke>("SELECT * FROM GetKe(@_objectid)", new{
            _objectid = objectid
        }, commandType: CommandType.Text);
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM Ke", commandType: CommandType.Text);
    }
    public int Add(Ke obj, int objectid){ 
        double? nulltoado = null;
        string? nullstring = null!;
        double? chieudai = obj.chieudai == null ? null : Convert.ToDouble(obj.chieudai);
        double? caotrinhdaykenh = obj.caotrinhdaykenh == null ? null : Convert.ToDouble(obj.caotrinhdaykenh);
        double? berongkenh = obj.berongkenh == null ? null : Convert.ToDouble(obj.berongkenh);
        double? hesomai = obj.hesomai == null ? null : Convert.ToDouble(obj.hesomai);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);

        if (obj.toado != null){
            return connection.ExecuteScalar<int>("AddKe", 
                new{
                    _objectid = objectid,
                    _idke = "KE" +  objectid.ToString(),
                    _tenkenhmuong = obj.tenkenhmuong,
                    _vitri = obj.vitri,
                    _chieudai = chieudai,
                    _caotrinhdaykenh = caotrinhdaykenh,
                    _berongkenh = berongkenh,
                    _hesomai = hesomai,
                    _caotrinhbotrai = obj.caotrinhbotrai,
                    _caotrinhbophai = obj.caotrinhbophai,
                    _berongbotrai = obj.berongbotrai,
                    _berongbophai = obj.berongbophai,
                    _hanhlangbaove = obj.hanhlangbaove,
                    _capcongtrinh = obj.capcongtrinh,
                    _cumcongtrinh = obj.cumcongtrinh,
                    _ketcaucongtrinh = obj.ketcaucongtrinh,
                    _muctieunhiemvu = obj.muctieunhiemvu,
                    _diadiem = obj.diadiem,
                    _namsudung = obj.namsudung,
                    _hethongcttl = obj.hethongcttl,
                    _donviquanly = obj.donviquanly,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring,
                    _shape_length = nulltoado,
                    _toado = obj.toado
                }, commandType: CommandType.StoredProcedure
            );
        }
        return connection.ExecuteScalar<int>("AddKe", 
            new{
                    _objectid = objectid,
                    _idke = "KE" +  objectid.ToString(),
                    _tenkenhmuong = obj.tenkenhmuong,
                    _vitri = obj.vitri,
                    _chieudai = chieudai,
                    _caotrinhdaykenh = caotrinhdaykenh,
                    _berongkenh = berongkenh,
                    _hesomai = hesomai,
                    _caotrinhbotrai = obj.caotrinhbotrai,
                    _caotrinhbophai = obj.caotrinhbophai,
                    _berongbotrai = obj.berongbotrai,
                    _berongbophai = obj.berongbophai,
                    _hanhlangbaove = obj.hanhlangbaove,
                    _capcongtrinh = obj.capcongtrinh,
                    _cumcongtrinh = obj.cumcongtrinh,
                    _ketcaucongtrinh = obj.ketcaucongtrinh,
                    _muctieunhiemvu = obj.muctieunhiemvu,
                    _diadiem = obj.diadiem,
                    _namsudung = obj.namsudung,
                    _hethongcttl = obj.hethongcttl,
                    _donviquanly = obj.donviquanly,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring,
                    _shape_length = nulltoado,
                    _toado = nullstring
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Edit(int objectid, Ke obj){ 
        double? nulltoado = null;
        string? nullstring = null!;
        double? chieudai = obj.chieudai == null ? null : Convert.ToDouble(obj.chieudai);
        double? caotrinhdaykenh = obj.caotrinhdaykenh == null ? null : Convert.ToDouble(obj.caotrinhdaykenh);
        double? berongkenh = obj.berongkenh == null ? null : Convert.ToDouble(obj.berongkenh);
        double? hesomai = obj.hesomai == null ? null : Convert.ToDouble(obj.hesomai);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);

        if (obj.toado != null){
            return connection.ExecuteScalar<int>("EditKe", 
                new{
                    _objectid = objectid,
                    _tenkenhmuong = obj.tenkenhmuong,
                    _vitri = obj.vitri,
                    _chieudai = chieudai,
                    _caotrinhdaykenh = caotrinhdaykenh,
                    _berongkenh = berongkenh,
                    _hesomai = hesomai,
                    _caotrinhbotrai = obj.caotrinhbotrai,
                    _caotrinhbophai = obj.caotrinhbophai,
                    _berongbotrai = obj.berongbotrai,
                    _berongbophai = obj.berongbophai,
                    _hanhlangbaove = obj.hanhlangbaove,
                    _capcongtrinh = obj.capcongtrinh,
                    _cumcongtrinh = obj.cumcongtrinh,
                    _ketcaucongtrinh = obj.ketcaucongtrinh,
                    _muctieunhiemvu = obj.muctieunhiemvu,
                    _diadiem = obj.diadiem,
                    _namsudung = obj.namsudung,
                    _hethongcttl = obj.hethongcttl,
                    _donviquanly = obj.donviquanly,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring,
                    _shape_length = nulltoado,
                    _toado = obj.toado
                }, commandType: CommandType.StoredProcedure
            );
        }
        return connection.ExecuteScalar<int>("EditKe", 
            new{
                    _objectid = objectid,
                    _tenkenhmuong = obj.tenkenhmuong,
                    _vitri = obj.vitri,
                    _chieudai = chieudai,
                    _caotrinhdaykenh = caotrinhdaykenh,
                    _berongkenh = berongkenh,
                    _hesomai = hesomai,
                    _caotrinhbotrai = obj.caotrinhbotrai,
                    _caotrinhbophai = obj.caotrinhbophai,
                    _berongbotrai = obj.berongbotrai,
                    _berongbophai = obj.berongbophai,
                    _hanhlangbaove = obj.hanhlangbaove,
                    _capcongtrinh = obj.capcongtrinh,
                    _cumcongtrinh = obj.cumcongtrinh,
                    _ketcaucongtrinh = obj.ketcaucongtrinh,
                    _muctieunhiemvu = obj.muctieunhiemvu,
                    _diadiem = obj.diadiem,
                    _namsudung = obj.namsudung,
                    _hethongcttl = obj.hethongcttl,
                    _donviquanly = obj.donviquanly,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring,
                    _shape_length = nulltoado,
                    _toado = nullstring
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeleteKe", new{
            _objectid = objectid
        });
    }
}