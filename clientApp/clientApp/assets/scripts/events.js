const events = [];

const showEventModalHandler = () => {
    fillCountrySelectList();
    addEventModal.classList.add('is-active');

    toggleBackdrop();
};

const toggleBackdrop = () => {
    backdrop.classList.toggle('is-block');
};

const cancelAddEventHandler = () => {
    closeEventModal();
    clearEventInput();

    toggleBackdrop();    
};

const cancelAddParticipantHandler = () => {
    closeParticipantModal();
    clearParticipantInput();

    toggleBackdrop();    
};

const cancelDeleteEventModalHandler = () => {
    closeDeleteEventModal();

    toggleBackdrop();    
};

const closeEventModal = () => {
    addEventModal.classList.remove('is-active');
}

const closeParticipantModal = () => {
    addParticipantModal.classList.remove('is-active');
}

const closeDeleteEventModal = () => {
    deleteEventModal.classList.remove('is-active');
}

const showFutureEvents = () => {
    toggleFutureEventsLi();
    togglePastEventsLi();
}

const showPastEvents = () => {
    toggleFutureEventsLi();
    togglePastEventsLi();
}

const toggleFutureEventsLi = () => {
    futureEventsLi.classList.toggle('is-active');
};

const togglePastEventsLi = () => {
    pastEventsLi.classList.toggle('is-active');
};

const loadEvents = () => {
    for(const event of events) {
        eventList.appendChild(event.render());
    }
}

const showDeleteEventModalHandler = (id) => {
    deleteEventModal.classList.add('is-active');

    toggleBackdrop();

    const cancelDeleteEventButton = document.getElementById('cancel-delete-event-button');
    let deleteEventButton = document.getElementById('delete-event-button');

    deleteEventButton.replaceWith(deleteEventButton.cloneNode(true));
    deleteEventButton = document.getElementById('delete-event-button');

    cancelDeleteEventButton.removeEventListener(
        'click',
        cancelDeleteEventModalHandler);
    
    cancelDeleteEventButton.addEventListener(
        'click',
        cancelDeleteEventModalHandler);
    deleteEventButton.addEventListener(
        'click',
        deleteEventHandler.bind(null, id));
};

const deleteEventHandler = (eventId) => {
    let eventIndex = 0;
    for (const event of events) {
        if (event.id === eventId) {
            break;
        }
        eventIndex++;
    }
    events.splice(eventIndex, 1);
    const listRoot = document.getElementById('event-list');
    listRoot.removeChild(listRoot.children[eventIndex]);

    closeDeleteEventModal();

    toggleBackdrop(); 
}

const backdropClickHandler = () => {
    console.log('here');
    closeEventModal();
    closeParticipantModal();

    clearEventInput();
    clearParticipantInput();

    closeDeleteEventModal();

    toggleBackdrop();    
};

startAddEventButton.addEventListener('click', showEventModalHandler);
cancelAddEventButton.addEventListener('click', cancelAddEventHandler);
backdrop.addEventListener('click', backdropClickHandler);

showFutureEvents();