import Docxtemplater from "docxtemplater";
import PizZip from "pizzip";
import PizZipUtils from "pizzip/utils";
import { saveAs } from "file-saver";
import { convertLanguages } from "src/tools/convertLanguages";

const docxType = {
  type: "blob",
  mimeType:
    "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
};
const xlsxType = {
  type: "blob",
  mimeType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
};
const formatType = {
  docx: docxType,
  xlsx: xlsxType,
};

const loadFile = (url, callback) => {
  PizZipUtils.getBinaryContent(url, callback);
};

const handledError = (error) => {
  const replaceErrors = (key, value) => {
    if (value instanceof Error) {
      return Object.getOwnPropertyNames(value).reduce((error, key) => {
        error[key] = value[key];
        return error;
      }, {});
    }
    return value;
  };
  console.log(JSON.stringify({ error: error }, replaceErrors));
  if (error.properties && error.properties.errors instanceof Array) {
    const errorMessages = error.properties.errors
      .map((error) => error.properties.explanation)
      .join("\n");
    console.log("errorMessages", errorMessages);
  }
  throw error;
};

export const generateDocument = ({ layer, data }) => {
  const format = "docx";
  const templateApi = `${process.env.REACT_APP_DOMAIN}${
    process.env.REACT_APP_API_REPORT
  }/${convertLanguages(layer)}.${format}`;

  loadFile(templateApi, (error, content) => {
    if (error) throw error;
    const initZip = new PizZip(content);
    const docFile = new Docxtemplater().loadZip(initZip);
    docFile.setData(data);
    try {
      docFile.render();
    } catch (error) {
      handledError(error);
    }
    const thisType = formatType[format];
    const outputFile = docFile.getZip().generate(thisType);
    saveAs(outputFile, `baocaothongke_${convertLanguages(layer)}.${format}`);
  });
};
