export const convertFormData = (data) => {
  const { file, ...infos } = data;
  const formData = new FormData();
  formData.append("file", file ?? null);
  const fields = Object.keys(infos);
  fields.forEach((field) => formData.append(field, infos[field] || null));
  return formData;
};
