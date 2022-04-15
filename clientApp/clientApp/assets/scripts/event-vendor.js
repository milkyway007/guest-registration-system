const backdrop = document.getElementById('backdrop');

const startAddEventButton = document.getElementById('start-add-event-button');
const addEventModal = document.getElementById('add-event-modal');
const cancelAddEventButton = document.getElementById('cancel-add-event-button');
const userEventInputs = addEventModal.querySelectorAll('input');


const addParticipantModal = document.getElementById('add-participant-modal');

const userParticipantInputs = addParticipantModal.querySelectorAll('input');

const deleteEventModal = document.getElementById('delete-event-modal');

const futureEventsLi = document.getElementById('future-events-li');
const pastEventsLi = document.getElementById('past-events-li');

const eventList = document.getElementById('event-list');
const buttons = document.getElementsByClassName('.event-element_action');