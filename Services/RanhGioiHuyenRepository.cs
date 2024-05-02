using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class RanhGioiHuyenRepository : BaseRepository{
    public RanhGioiHuyenRepository(IDbConnection connection) : base(connection){}

    public IEnumerable<RanhGioiHuyen> GetHuyens(){
        //return connection.Query<RanhGioiHuyen>("select objectid, mahuyen, tenhuyen, dientichtunhien, shape_length, shape_area, st_asgeojson(st_transform(shape, 4326)) as shape  from rghuyen");
        return connection.Query<RanhGioiHuyen>("SELECT * FROM GetHuyens()");
    }
    public RanhGioiHuyen? GetHuyen(string txt){
        //return connection.Query<RanhGioiHuyen>("select objectid, mahuyen, tenhuyen, dientichtunhien, shape_length, shape_area, st_asgeojson(st_transform(shape, 4326)) as shape  from rghuyen");
        return connection.Query<RanhGioiHuyen>("SELECT * FROM GetHuyen(@_mahuyen)", new {
            _mahuyen = txt
        }).FirstOrDefault();
    }
}