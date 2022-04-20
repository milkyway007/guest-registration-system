import { renderParticipants } from "./participant_renderer.js";

let participants = [];

const updateParticipantAtIndex = (index, participant) => {
  participants[index] = participant;
  console.log(participants[index]);
};

const deleteParticipantAtIndex = (index) => {
  participants.splice(index, 1);
};

const findParticipantIndex = (code) => {
  let participantIndex = 0;
  for (const participant of participants) {
    if (participant.code === code) {
      break;
    }
    participantIndex++;
  }

  return participantIndex;
};

const sortPersons = (a, b) => {
  if (a.firstName > b.firstName) {
    return 1;
  }
  if (a.firstName < b.firstName) {
    return -1;
  }

  return 0;
};

const sortCompanies = (a, b) => {
  if (a.name > b.name) {
    return 1;
  }
  if (a.name < b.name) {
    return -1;
  }

  return 0;
};

const emptyParticipants = () => {
  participants = [];
};

const addParticipant = (participant) => {
  participants.push(participant);
};

const renderSortedParticipants = (condition) => {
  if (condition === "person") {
    participants.sort(sortPersons);
  } else if (condition === "company") {
    participants.sort(sortCompanies);
  }

  renderParticipants(participants, condition);
};

export {
  emptyParticipants,
  addParticipant,
  renderSortedParticipants,
  deleteParticipantAtIndex,
  findParticipantIndex,
  updateParticipantAtIndex,
};
