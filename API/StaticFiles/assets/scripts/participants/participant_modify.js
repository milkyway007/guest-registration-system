import {
  initializeFields,
  disableParticipantInput,
  enableParticipantInput,
  update,
  closeParticipantDetailsModal,
  showParticipantDetailsModal,
  showProperForm,
  clearParticipantValidation,
  isPersonFormValid,
  isCompanyFormValid,
} from "./participant_details_modal.js";
import {
  updateParticipantAtIndex,
  findParticipantIndex,
} from "./participant_container.js";
import { updateParticipantElement } from "./participant_renderer.js";

const updateUiAfterParticipatModify = (participant, condition) => {
  let participantIndex = findParticipantIndex(participant.code);
  updateParticipantAtIndex(participantIndex, participant);

  updateParticipantElement(participantIndex, participant, condition);
};

function closeViewParticipantModal() {
  const closeParticipantsDetailsButton = document.getElementById(
    "close-participants-details-button"
  );
  const cancelModifyParticipantButton = document.getElementById(
    "cancel-modify-participant-button"
  );
  let startModifyParticipantButton = document.getElementById(
    "start-modify-participant-button"
  );
  let saveParticipantButton = document.getElementById(
    "save-participant-button"
  );

  if (
    !closeParticipantsDetailsButton &&
    !cancelModifyParticipantButton &&
    !startModifyParticipantButton &&
    !saveParticipantButton
  ) {
    return;
  }
  closeParticipantsDetailsButton.classList.remove("is-hidden");
  cancelModifyParticipantButton.classList.add("is-hidden");
  startModifyParticipantButton.classList.remove("is-hidden");
  saveParticipantButton.classList.add("is-hidden");

  closeParticipantDetailsModal();
}

const saveParticipantHandler = (person, condition) => {
  if (
    (condition === "person" && !isPersonFormValid()) ||
    (condition === "company" && !isCompanyFormValid())
  ) {
    return;
  }

  update(person, condition);
};

const startModifyParticipanttHandler = () => {
  const closeParticipantsDetailsButton = document.getElementById(
    "close-participants-details-button"
  );
  const cancelModifyParticipantButton = document.getElementById(
    "cancel-modify-participant-button"
  );
  let startModifyParticipantButton = document.getElementById(
    "start-modify-participant-button"
  );
  let saveParticipantButton = document.getElementById(
    "save-participant-button"
  );

  closeParticipantsDetailsButton.classList.add("is-hidden");
  cancelModifyParticipantButton.classList.remove("is-hidden");
  startModifyParticipantButton.classList.add("is-hidden");
  saveParticipantButton.classList.remove("is-hidden");

  enableParticipantInput();
};

const showViewParticipantModalHandler = (participant, condition) => {
  console.log(participant);
  clearParticipantValidation();
  showProperForm(condition);
  initializeFields(participant, condition);
  disableParticipantInput();

  showParticipantDetailsModal();

  const closeParticipantsDetailsButton = document.getElementById(
    "close-participants-details-button"
  );
  const cancelModifyParticipantButton = document.getElementById(
    "cancel-modify-participant-button"
  );
  let startModifyParticipantButton = document.getElementById(
    "start-modify-participant-button"
  );
  let saveParticipantButton = document.getElementById(
    "save-participant-button"
  );

  startModifyParticipantButton.replaceWith(
    startModifyParticipantButton.cloneNode(true)
  );
  startModifyParticipantButton = document.getElementById(
    "start-modify-participant-button"
  );
  saveParticipantButton.replaceWith(saveParticipantButton.cloneNode(true));
  saveParticipantButton = document.getElementById("save-participant-button");

  closeParticipantsDetailsButton.removeEventListener(
    "click",
    closeViewParticipantModal
  );
  closeParticipantsDetailsButton.addEventListener(
    "click",
    closeViewParticipantModal
  );
  cancelModifyParticipantButton.removeEventListener(
    "click",
    closeViewParticipantModal
  );
  cancelModifyParticipantButton.addEventListener(
    "click",
    closeViewParticipantModal
  );
  startModifyParticipantButton.addEventListener(
    "click",
    startModifyParticipanttHandler.bind(null)
  );

  saveParticipantButton.addEventListener(
    "click",
    saveParticipantHandler.bind(null, participant, condition)
  );
};

export {
  showViewParticipantModalHandler,
  updateUiAfterParticipatModify,
  closeViewParticipantModal,
};
