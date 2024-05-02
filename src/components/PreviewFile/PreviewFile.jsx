import { Fragment, useState } from "react";
import { Image, PDF, Video } from "src/components/interfaces/Render";
import Modal from "src/components/interfaces/Modal";
import { Button, ButtonGroup } from "src/components/interfaces/Button";

const PreviewFile = ({ file, onClear }) => {
  const [isOpenFile, toggleOpenFile] = useState(false);
  return (
    file && (
      <Fragment>
        <ButtonGroup>
          <Button primary onClick={() => toggleOpenFile(true)}>
            Xem tệp
          </Button>
          <Button danger onClick={onClear}>
            Xóa tệp
          </Button>
        </ButtonGroup>
        <Modal isOpen={isOpenFile} onClose={() => toggleOpenFile(false)}>
          <Modal.Header>Tư liệu</Modal.Header>
          <Modal.Body>
            <div className="file-render">
              {file?.type?.includes("video") && (
                <Video width="100%" height="100%" source={file?.source} />
              )}
              {file?.type?.includes("image") && (
                <Image width="100%" height="100%" source={file?.source} />
              )}
              {file?.type?.includes("pdf") && (
                <PDF width="100%" height="100%" source={file?.source} />
              )}
            </div>
          </Modal.Body>
        </Modal>
      </Fragment>
    )
  );
};

export default PreviewFile;
