import {
  removeObjDuplicates,
  removeValDuplicates,
} from "src/tools/removeDuplicates";

export const getViewLayers = ({ pageType, roleLists }) => {
  const viewRole = roleLists?.filter(
    (r) => r.roleid.toString() === process.env.REACT_APP_VIEWROLE
  );
  switch (pageType) {
    case process.env.REACT_APP_HOMEPAGE:
      const arrHomeDistricts = viewRole.map((r) => r["tenhuyen"]);
      const arrHomeGroupLayer = viewRole.map((r) => r["nhomdulieu"]);
      const homeDistricts = removeValDuplicates(arrHomeDistricts);
      const homeGroupLayers = removeValDuplicates(arrHomeGroupLayer);

      const homeDistrictID = (d) => {
        const thisDistrict = viewRole?.find((r) => r["tenhuyen"] === d);
        return thisDistrict["maquanhuyen"];
      };
      const homeDistrictIDOrigin = (d) => {
        const thisDistrict = viewRole?.find((r) => r["tenhuyen"] === d);
        return thisDistrict["mahuyen"];
      };
      const homeGroupID = (g) => {
        const thisGroup = viewRole?.find((r) => r["nhomdulieu"] === g);
        return thisGroup["malopdoituong"];
      };
      const homeLayers = (d, g) => {
        return viewRole
          ?.filter((r) => r["tenhuyen"] === d && r["nhomdulieu"] === g)
          ?.map((r) => ({
            layerName: r["lopdulieu"],
            layerID: r["malopdulieu"],
          }));
      };
      const homeGroups = (d) => {
        return homeGroupLayers?.map((g) => ({
          groupName: g,
          groupID: homeGroupID(g),
          layers: homeLayers(d, g),
        }));
      };
      const homeDistrict = homeDistricts?.map((d) => ({
        districtName: d,
        districtID: homeDistrictID(d),
        districtIDOrigin: homeDistrictIDOrigin(d),
        groups: homeGroups(d),
      }));
      const [saveHomeDistrict] = homeDistrict;
      const homeFullZone = {
        ...saveHomeDistrict,
        districtName: process.env.REACT_APP_FULLZONETITLE,
        districtID: process.env.REACT_APP_FULLZONENAME,
        districtIDOrigin: "null",
      };
      const allHomeDistricts = [[...homeDistrict, homeFullZone]];
      const homeArguments = { districts: allHomeDistricts };
      return homeArguments;

    case process.env.REACT_APP_SOLUTIONPAGE:
      const arrSolutionDistricts = viewRole.map((r) => r["tenhuyen"]);
      const solutionDistricts = removeValDuplicates(arrSolutionDistricts);
      const solutionLayer = (d) => {
        return viewRole
          ?.filter((r) => r["tenhuyen"] === d)
          ?.map((r) => ({
            layerName: r["lopdulieu"],
            layerID: r["malopdulieu"],
          }));
      };
      const solutionDistrictID = (d) => {
        const thisDistrict = viewRole?.find((r) => r["tenhuyen"] === d);
        return thisDistrict["mahuyen"];
      };
      const solutionDistrict = solutionDistricts.map((d) => ({
        districtName: d,
        districtID: solutionDistrictID(d),
        layers: solutionLayer(d),
      }));
      const [saveSolutionDistrict] = solutionDistrict;
      const solutionFullZone = {
        ...saveSolutionDistrict,
        districtName: process.env.REACT_APP_FULLZONETITLE,
        districtID: "null",
      };
      const allSolutionDistricts = [[...solutionDistrict, solutionFullZone]];
      const solutionArguments = { districts: allSolutionDistricts };
      return solutionArguments;

    case process.env.REACT_APP_MATERIALPAGE:
      const materialDistricts = removeObjDuplicates(viewRole, "mahuyen");
      const materialLayers = removeObjDuplicates(viewRole, "lopdulieu");
      const materialDistrict = materialDistricts.map((r) => ({
        districtName: r["tenhuyen"],
        districtID: r["mahuyen"],
      }));
      const materialLayer = materialLayers.map((r) => ({
        layerName: r["lopdulieu"],
      }));
      const materialFullZone = {
        districtName: process.env.REACT_APP_FULLZONETITLE,
        districtID: "null",
      };
      const allMaterialDistricts = [[...materialDistrict, materialFullZone]];
      const allMaterialLayers = [[...materialLayer]];
      const materialArguments = {
        layers: allMaterialLayers,
        districts: allMaterialDistricts,
      };
      return materialArguments;

    case process.env.REACT_APP_ADMINPAGE:
      const adminLayers = removeObjDuplicates(viewRole, "lopdulieu");
      const adminLayer = adminLayers.map((r) => ({
        layerName: r["lopdulieu"],
        tableName: r["tengocbang"]
      }));
      const allAdminLayers = [[...adminLayer]];
      const adminArguments = { layers: allAdminLayers };
      return adminArguments;

    default:
      throw new Error("Invalid configuration");
  }
};
