import {
  closeParticipantDetailsModal,
  isPersonFormValid,
  isCompanyFormValid,
  createPerson,
  createCompany,
  isPersonActive,
  showParticipantDetailsModal,
} from "./participant_details_modal.js";
import { postPerson, postCompany } from "../requests/participants/post.js";

const addParticipantHandler = (eventId) => {
  const condition = isPersonActive() ? "person" : "company";
  if (condition === "person") {
    if (!isPersonFormValid()) {
      return;
    }

    const person = createPerson();
    postPerson(eventId, person);
  } else {
    if (!isCompanyFormValid()) {
      return;
    }

    const company = createCompany();
    postCompany(eventId, company);
  }
};

const showAddParticipantModalHandler = (eventId) => {
  showParticipantDetailsModal();

  const cancelAddParticipantButton = document.getElementById(
    "cancel-add-participant-button"
  );
  let addParticipantButton = document.getElementById("add-participant-button");

  addParticipantButton.replaceWith(addParticipantButton.cloneNode(true));
  addParticipantButton = document.getElementById("add-participant-button");

  cancelAddParticipantButton.removeEventListener(
    "click",
    closeParticipantDetailsModal
  );

  cancelAddParticipantButton.addEventListener(
    "click",
    closeParticipantDetailsModal
  );
  addParticipantButton.addEventListener(
    "click",
    addParticipantHandler.bind(null, eventId)
  );
};

export { showAddParticipantModalHandler };
