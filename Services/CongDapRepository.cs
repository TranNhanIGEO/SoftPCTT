using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class CongDapRepository : BaseRepository{
    public CongDapRepository(IDbConnection connection) : base(connection){}
    public IEnumerable<CongDap> GetCongDaps(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<CongDap>("SELECT a.objectid, a.idcongdap, a.tencongdap, a.lytrinh,  ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.cumcongtrinh, a.goithau, a.loaicongtrinh, a.hinhthuc, a.chieudai, a.duongkinh, a.berong, a.chieucao, a.socua, a.caotrinhdaycong, a.caotrinhdinhcong, a.hinhthucvanhanh, a.muctieunhiemvu, a.diadiem, a.namsudung, a.capcongtrinh, a.hethongcttl, a.donviquanly, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM CongDap a LEFT JOIN rghuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326)) WHERE h.MaHuyen = @_mahuyen)", new{
                    _mahuyen = mahuyen
                });
            }
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<CongDap>("SELECT * FROM GetCongDaps(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<CongDap>("SELECT a.objectid, a.idcongdap, a.tencongdap, a.lytrinh,  ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.cumcongtrinh, a.goithau, a.loaicongtrinh, a.hinhthuc, a.chieudai, a.duongkinh, a.berong, a.chieucao, a.socua, a.caotrinhdaycong, a.caotrinhdinhcong, a.hinhthucvanhanh, a.muctieunhiemvu, a.diadiem, a.namsudung, a.capcongtrinh, a.hethongcttl, a.donviquanly, a.namcapnhat, a.ghichu, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM CongDap a LEFT JOIN rghuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326)) WHERE " + SqlQuery + "");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<CongDap>("SELECT * FROM GetCongDaps(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public IEnumerable<SewerDetailStatistics> GetSewerDetailStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<SewerDetailStatistics>("SELECT a.donviquanly, a.capcongtrinh, COUNT(CASE WHEN a.capcongtrinh IS NULL THEN '0' ELSE a.capcongtrinh END) AS tongsocapcongtrinh, SUM(a.chieudai)::Numeric AS tongchieudaitheocapcongtrinh, (CASE WHEN c.color ISNULL THEN (SELECT color FROM color where name = 'Đang cập nhật') ELSE c.color END) AS mamau FROM CongDap a LEFT JOIN Color c ON a.capcongtrinh = c.name LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326)) WHERE h.MaHuyen = @_mahuyen AND " + SqlQuery + " GROUP BY a.donviquanly, a.capcongtrinh, c.color ORDER BY a.donviquanly ASC, a.capcongtrinh ASC"
                , new{
                    _mahuyen = mahuyen
                });
            }
                return connection.Query<SewerDetailStatistics>("SELECT a.donviquanly, a.capcongtrinh, COUNT(CASE WHEN a.capcongtrinh IS NULL THEN '0' ELSE a.capcongtrinh END) AS tongsocapcongtrinh, SUM(a.chieudai)::Numeric AS tongchieudaitheocapcongtrinh, (CASE WHEN c.color ISNULL THEN (SELECT color FROM color where name = 'Đang cập nhật') ELSE c.color END) AS mamau FROM CongDap a LEFT JOIN Color c ON a.capcongtrinh = c.name LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326)) WHERE h.MaHuyen = @_mahuyen GROUP BY a.donviquanly, a.capcongtrinh, c.color ORDER BY a.donviquanly ASC, a.capcongtrinh ASC"
                , new{
                    _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<SewerDetailStatistics>("SELECT a.donviquanly, a.capcongtrinh, COUNT(CASE WHEN a.capcongtrinh IS NULL THEN '0' ELSE a.capcongtrinh END) AS tongsocapcongtrinh, SUM(a.chieudai)::Numeric AS tongchieudaitheocapcongtrinh, (CASE WHEN c.color ISNULL THEN (SELECT color FROM color where name = 'Đang cập nhật') ELSE c.color END) AS mamau FROM CongDap a LEFT JOIN Color c ON a.capcongtrinh = c.name LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326)) WHERE " + SqlQuery + " GROUP BY a.donviquanly, a.capcongtrinh, c.color ORDER BY a.donviquanly ASC, a.capcongtrinh ASC");            
            }
            return connection.Query<SewerDetailStatistics>("SELECT a.donviquanly, a.capcongtrinh, COUNT(CASE WHEN a.capcongtrinh IS NULL THEN '0' ELSE a.capcongtrinh END) AS tongsocapcongtrinh, SUM(a.chieudai)::Numeric AS tongchieudaitheocapcongtrinh, (CASE WHEN c.color ISNULL THEN (SELECT color FROM color where name = 'Đang cập nhật') ELSE c.color END) AS mamau FROM CongDap a LEFT JOIN Color c ON a.capcongtrinh = c.name LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326)) GROUP BY a.donviquanly, a.capcongtrinh, c.color ORDER BY a.donviquanly ASC, a.capcongtrinh ASC");  
        }       
    } 
    public IEnumerable<SewerTotalStatistics> GetSewerTotalStatistics(string mahuyen, string? SqlQuery){       
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<SewerTotalStatistics>("SELECT a.capcongtrinh, COUNT(CASE WHEN a.capcongtrinh IS NULL THEN '0' ELSE a.capcongtrinh END) AS tongsocapcongtrinh, SUM(a.chieudai)::Numeric AS tongchieudaitheocapcongtrinh, (CASE WHEN c.color ISNULL THEN (SELECT color FROM color where name = 'Đang cập nhật') ELSE c.color END) AS mamau, 'Cấp công trình' AS phamvithongke FROM CongDap a LEFT JOIN Color c ON a.capcongtrinh = c.name LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326)) WHERE " + SqlQuery + " AND h.MaHuyen = @_mahuyen GROUP BY a.capcongtrinh, c.color ORDER BY a.capcongtrinh ASC"
                , new{
                    _mahuyen = mahuyen
                });
            }
                return connection.Query<SewerTotalStatistics>("SELECT a.capcongtrinh, COUNT(CASE WHEN a.capcongtrinh IS NULL THEN '0' ELSE a.capcongtrinh END) AS tongsocapcongtrinh, SUM(a.chieudai)::Numeric AS tongchieudaitheocapcongtrinh, (CASE WHEN c.color ISNULL THEN (SELECT color FROM color where name = 'Đang cập nhật') ELSE c.color END) AS mamau, 'Cấp công trình' AS phamvithongke FROM CongDap a LEFT JOIN Color c ON a.capcongtrinh = c.name LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326)) WHERE h.MaHuyen = @_mahuyen GROUP BY a.capcongtrinh, c.color ORDER BY a.capcongtrinh ASC"
                , new{
                    _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<SewerTotalStatistics>("SELECT a.capcongtrinh, COUNT(CASE WHEN a.capcongtrinh IS NULL THEN '0' ELSE a.capcongtrinh END) AS tongsocapcongtrinh, SUM(a.chieudai)::Numeric AS tongchieudaitheocapcongtrinh, (CASE WHEN c.color ISNULL THEN (SELECT color FROM color where name = 'Đang cập nhật') ELSE c.color END) AS mamau, 'Cấp công trình' AS phamvithongke FROM CongDap a LEFT JOIN Color c ON a.capcongtrinh = c.name WHERE " + SqlQuery + " GROUP BY a.capcongtrinh, c.color ORDER BY a.capcongtrinh ASC");            
            }
            return connection.Query<SewerTotalStatistics>("SELECT a.capcongtrinh, COUNT(CASE WHEN a.capcongtrinh IS NULL THEN '0' ELSE a.capcongtrinh END) AS tongsocapcongtrinh, SUM(a.chieudai)::Numeric AS tongchieudaitheocapcongtrinh, (CASE WHEN c.color ISNULL THEN (SELECT color FROM color where name = 'Đang cập nhật') ELSE c.color END) AS mamau, 'Cấp công trình' AS phamvithongke FROM CongDap a LEFT JOIN Color c ON a.capcongtrinh = c.name GROUP BY a.capcongtrinh, c.color ORDER BY a.capcongtrinh ASC");  
        }          
    }
    public CongDap? GetCongDap(int objectid){
        return connection.QueryFirstOrDefault<CongDap>("SELECT * FROM GetCongDap(@_objectid)", new{
            _objectid = objectid
        });
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM CongDap", commandType: CommandType.Text);
    } 
    public int Add(CongDap obj){  
        double? nulltoado = null;
        string? nullstring = null!;
        double? chieudai = obj.chieudai == null ? null : Convert.ToDouble(obj.chieudai);
        double? duongkinh = obj.duongkinh == null ? null : Convert.ToDouble(obj.duongkinh);
        double? berong = obj.berong == null ? null : Convert.ToDouble(obj.berong);  
        double? chieucao = obj.chieucao == null ? null : Convert.ToDouble(obj.chieucao);
        short? socua = obj.socua == null ? null : Convert.ToInt16(obj.socua);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        double? toadox = obj.toadox == null ? null : Convert.ToDouble(obj.toadox);
        double? toadoy = obj.toadoy == null ? null : Convert.ToDouble(obj.toadoy);
        
        if (obj.toadox != null && obj.toadoy != null){
            return connection.ExecuteScalar<int>("AddCongDap", 
                new{
                    _objectid  = obj.objectid,
                    _idcongdap = "CD" +  obj.objectid.ToString(),
                    _tencongdap = obj.tencongdap,
                    _lytrinh = obj.lytrinh,
                    _toadox = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadox), 3)),
                    _toadoy = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadoy), 3)),
                    _cumcongtrinh = obj.cumcongtrinh,
                    _goithau = obj.goithau,
                    _loaicongtrinh = obj.loaicongtrinh,
                    _hinhthuc = obj.hinhthuc,
                    _chieudai = chieudai,
                    _duongkinh = duongkinh,
                    _berong = berong,
                    _chieucao = chieucao,
                    _socua = socua,
                    _caotrinhdaycong = obj.caotrinhdaycong,
                    _caotrinhdinhcong = obj.caotrinhdinhcong,
                    _hinhthucvanhanh = obj.hinhthucvanhanh,
                    _muctieunhiemvu = obj.muctieunhiemvu,
                    _diadiem = obj.diadiem,
                    _namsudung = obj.namsudung,
                    _capcongtrinh = obj.capcongtrinh,
                    _hethongcttl = obj.hethongcttl,
                    _donviquanly = obj.donviquanly,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring
                }, commandType: CommandType.StoredProcedure
            );
        }

        return connection.ExecuteScalar<int>("AddCongDap", 
            new{
                _objectid  = obj.objectid,
                _idcongdap = "CD" +  obj.objectid.ToString(),
                _tencongdap = obj.tencongdap,
                _lytrinh = obj.lytrinh,
                _toadox = nulltoado,
                _toadoy = nulltoado,
                _cumcongtrinh = obj.cumcongtrinh,
                _goithau = obj.goithau,
                _loaicongtrinh = obj.loaicongtrinh,
                _hinhthuc = obj.hinhthuc,
                _chieudai = chieudai,
                _duongkinh = duongkinh,
                _berong = berong,
                _chieucao = chieucao,
                _socua = socua,
                _caotrinhdaycong = obj.caotrinhdaycong,
                _caotrinhdinhcong = obj.caotrinhdinhcong,
                _hinhthucvanhanh = obj.hinhthucvanhanh,
                _muctieunhiemvu = obj.muctieunhiemvu,
                _diadiem = obj.diadiem,
                _namsudung = obj.namsudung,
                _capcongtrinh = obj.capcongtrinh,
                _hethongcttl = obj.hethongcttl,
                _donviquanly = obj.donviquanly,
                _namcapnhat = namcapnhat,
                _ghichu = nullstring

            }, commandType: CommandType.StoredProcedure
        );
    } 
    public int Edit(int objectid, CongDap obj){  
        double? nulltoado = null;
        string? nullstring = null!;
        double? chieudai = obj.chieudai == null ? null : Convert.ToDouble(obj.chieudai);
        double? duongkinh = obj.duongkinh == null ? null : Convert.ToDouble(obj.duongkinh);
        double? berong = obj.berong == null ? null : Convert.ToDouble(obj.berong);  
        double? chieucao = obj.chieucao == null ? null : Convert.ToDouble(obj.chieucao);
        short? socua = obj.socua == null ? null : Convert.ToInt16(obj.socua);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);
        double? toadox = obj.toadox == null ? null : Convert.ToDouble(obj.toadox);
        double? toadoy = obj.toadoy == null ? null : Convert.ToDouble(obj.toadoy);
        
        if (obj.toadox != null && obj.toadoy != null){
            return connection.ExecuteScalar<int>("EditCongDap", 
                new{
                    _objectid  = objectid,
                    _tencongdap = obj.tencongdap,
                    _lytrinh = obj.lytrinh,
                    _toadox = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadox), 3)),
                    _toadoy = Convert.ToDouble(Math.Round(Convert.ToDecimal(toadoy), 3)),
                    _cumcongtrinh = obj.cumcongtrinh,
                    _goithau = obj.goithau,
                    _loaicongtrinh = obj.loaicongtrinh,
                    _hinhthuc = obj.hinhthuc,
                    _chieudai = chieudai,
                    _duongkinh = duongkinh,
                    _berong = berong,
                    _chieucao = chieucao,
                    _socua = socua,
                    _caotrinhdaycong = obj.caotrinhdaycong,
                    _caotrinhdinhcong = obj.caotrinhdinhcong,
                    _hinhthucvanhanh = obj.hinhthucvanhanh,
                    _muctieunhiemvu = obj.muctieunhiemvu,
                    _diadiem = obj.diadiem,
                    _namsudung = obj.namsudung,
                    _capcongtrinh = obj.capcongtrinh,
                    _hethongcttl = obj.hethongcttl,
                    _donviquanly = obj.donviquanly,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring
                }, commandType: CommandType.StoredProcedure
            );
        }

        return connection.ExecuteScalar<int>("EditCongDap", 
            new{
                _objectid  = objectid,
                _tencongdap = obj.tencongdap,
                _lytrinh = obj.lytrinh,
                _toadox = nulltoado,
                _toadoy = nulltoado,
                _cumcongtrinh = obj.cumcongtrinh,
                _goithau = obj.goithau,
                _loaicongtrinh = obj.loaicongtrinh,
                _hinhthuc = obj.hinhthuc,
                _chieudai = chieudai,
                _duongkinh = duongkinh,
                _berong = berong,
                _chieucao = chieucao,
                _socua = socua,
                _caotrinhdaycong = obj.caotrinhdaycong,
                _caotrinhdinhcong = obj.caotrinhdinhcong,
                _hinhthucvanhanh = obj.hinhthucvanhanh,
                _muctieunhiemvu = obj.muctieunhiemvu,
                _diadiem = obj.diadiem,
                _namsudung = obj.namsudung,
                _capcongtrinh = obj.capcongtrinh,
                _hethongcttl = obj.hethongcttl,
                _donviquanly = obj.donviquanly,
                _namcapnhat = namcapnhat,
                _ghichu = nullstring

            }, commandType: CommandType.StoredProcedure
        );
    } 
    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeleteCongDap", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
}