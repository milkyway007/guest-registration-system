import { closeParticipantDetailsModal } from "./participants/participant_details_modal.js";
import { closeAddEventModal } from "./events/event_add.js";
import { closeDeleteEventModal } from "./events/event_delete.js";
import { hideMessage } from "./message.js";
import { closeViewParticipantModal } from "./participants/participant_modify.js";
import { closeDeleteParticipantModal } from "./participants/participant_delete.js";

const backdrops = document.querySelectorAll(".backdrop");

const isAnyBackdropActive = () => {
  for (const backdrop of backdrops) {
    if (backdrop.parentElement.classList.contains("is-active")) {
      return true;
    }
  }

  return false;
};

const backdropClickHandler = () => {
  console.log("2");
  closeDeleteParticipantModal();
  closeViewParticipantModal();
  closeDeleteEventModal();
  closeParticipantDetailsModal();
  closeAddEventModal();
  hideMessage();
};

export { backdropClickHandler, isAnyBackdropActive };
