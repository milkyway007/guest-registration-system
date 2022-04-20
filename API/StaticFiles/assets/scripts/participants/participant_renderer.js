import { showDeleteParticipantModalHandler } from "./participant_delete.js";
import { showViewParticipantModalHandler } from "./participant_modify.js";

const participantRoot = document.getElementById("participant-list");

const updateParticipantElement = (index, participant, condition) => {
  const participantElement = createParticipanttElement(participant, condition);
  participantRoot.replaceChild(
    participantElement,
    participantRoot.children[index]
  );
  console;
};

const deleteParticipantElement = (index) => {
  participantRoot.removeChild(participantRoot.children[index]);
};

const createParticipanttElement = (participant, condition) => {
  var participantElement = participant.render();

  const startDeleteParticipantButton = participantElement.querySelector(
    ".start-delete-participant-button"
  );
  const startModifyParticipantButton = participantElement.querySelector(
    ".participant-details-button"
  );

  startDeleteParticipantButton.addEventListener(
    "click",
    showDeleteParticipantModalHandler.bind(null, participant.code)
  );
  startModifyParticipantButton.addEventListener(
    "click",
    showViewParticipantModalHandler.bind(null, participant, condition)
  );

  return participantElement;
};

const renderParticipant = (participant, condition) => {
  var participantElement = createParticipanttElement(participant, condition);

  participantRoot.appendChild(participantElement);
};

const emptyParticipantRoot = () => {
  removeChildren(participantRoot);
};

const renderParticipants = (participants, condition) => {
  emptyParticipantRoot();
  for (const participant of participants) {
    renderParticipant(participant, condition);
  }
};

export {
  renderParticipants,
  emptyParticipantRoot,
  renderParticipant,
  deleteParticipantElement,
  updateParticipantElement,
};
