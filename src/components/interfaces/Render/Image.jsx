import "./Render.css";
import { useEffect, useState } from "react";
import imageCompression from "browser-image-compression";

const fetchImageAsBlob = async (image) => {
  const imageUrl = new Request(image);
  const response = await fetch(imageUrl);
  const blob = await response.blob();
  return blob;
};

const compressBlobImage = async (blob) => {
  const options = {
    maxSizeMB: 1,
    maxWidthOrHeight: 720,
    useWebWorker: true,
  };
  const compressedFile = await imageCompression(blob, options);
  return compressedFile;
};

const Image = (props) => {
  const { width, height, source } = props;
  const [compressedImage, setCompressedImage] = useState(null);

  useEffect(() => {
    let blobUrl = null;

    const processImage = async () => {
      try {
        const blob = await fetchImageAsBlob(source);
        const compressedFile = await compressBlobImage(blob);
        blobUrl = URL.createObjectURL(compressedFile);
        setCompressedImage(blobUrl);
      } catch (error) {
        console.log("Image processing failed:", error);
      }
    };

    processImage();
    return () => URL.revokeObjectURL(blobUrl);
  }, [source]);

  return (
    <div className="image-render">
      {compressedImage ? (
        <img
          height={height}
          width={width}
          src={compressedImage}
          alt=""
        />
      ) : (
        "Loading..."
      )}
    </div>
  );
};

export default Image;
