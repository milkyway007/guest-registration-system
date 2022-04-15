const participantDetailsButton = document.getElementById('participant-details-button');
//const participantDetailsModal = document.getElementById('participant-details-modal');

const closeButton = document.getElementById('close-participants-details-button');
const cancelModifyParticipantButton = document.getElementById('cancel-modify-participant-button');
const startModifyParticipantButton = document.getElementById('start-modify-participant-button');

const cancelDeleteParticipantButton = document.getElementById('cancel-delete-participant-button');
const participantDeleteModal = document.getElementById('delete-participant-modal');

const userParticipantInputs = participantDetailsModal.querySelectorAll('input');
const participantSelects = participantDetailsModal.querySelectorAll('select');
const participantTextareas = participantDetailsModal.querySelectorAll('textarea');

const startDeleteParticipantButton = document.getElementById('start-delete-participant-button');
const deleteParticipantModal = document.getElementById('delete-participant-modal');
const cancelDeleteEventButton = document.getElementById('cancel-delete-participant-button');

var personLi = document.getElementById('person-li');
var companyLi = document.getElementById('company-li');

var participantsList = document.getElementById('participant-list');