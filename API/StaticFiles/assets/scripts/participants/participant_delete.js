import { deleteParticipantElement } from "./participant_renderer.js";
import {
  deleteParticipantAtIndex,
  findParticipantIndex,
} from "./participant_container.js";
import { deleteParticipant } from "./../requests/participants/delete.js";

const deleteParticipantModal = document.getElementById(
  "delete-participant-modal"
);

const updateUiAfterParticipantdelete = (participantCode) => {
  let participantIndex = findParticipantIndex(participantCode);
  deleteParticipantAtIndex(participantIndex);

  deleteParticipantElement(participantIndex);
};

const closeDeleteParticipantModal = () => {
  if (!deleteParticipantModal) {
    return;
  }
  deleteParticipantModal.classList.remove("is-active");
};

const deleteParticipantHandler = (code) => {
  var eventId = getQueryParamByKey("event_id");
  deleteParticipant(eventId, code);
};

const showDeleteParticipantModalHandler = (code) => {
  deleteParticipantModal.classList.add("is-active");

  const cancelDeleteParticipantButton = document.getElementById(
    "cancel-delete-participant-button"
  );
  let deleteParticipantButton = document.getElementById(
    "delete-participant-button"
  );
  deleteParticipantButton.replaceWith(deleteParticipantButton.cloneNode(true));
  deleteParticipantButton = document.getElementById(
    "delete-participant-button"
  );

  cancelDeleteParticipantButton.removeEventListener(
    "click",
    closeDeleteParticipantModal
  );
  cancelDeleteParticipantButton.addEventListener(
    "click",
    closeDeleteParticipantModal
  );

  deleteParticipantButton.addEventListener(
    "click",
    deleteParticipantHandler.bind(null, code)
  );
};

export {
  showDeleteParticipantModalHandler,
  updateUiAfterParticipantdelete,
  closeDeleteParticipantModal,
};
