const participants = [];

const showParticipantDetailsModalHandler = (participant) => {
    initializeFields(participant);
    disableParticipantInput();

    participantDetailsModal.classList.add('is-active');

    toggleBackdrop();

    const closeParticipantsDetailsButton = document.getElementById('close-participants-details-button');
    const cancelModifyParticipantButton = document.getElementById('cancel-modify-participant-button');
    let startModifyParticipantButton = document.getElementById('start-modify-participant-button');
    let saveParticipantButton = document.getElementById('save-participant-button');

    startModifyParticipantButton.replaceWith(startModifyParticipantButton.cloneNode(true));
    startModifyParticipantButton = document.getElementById('start-modify-participant-button');

    saveParticipantButton.replaceWith(saveParticipantButton.cloneNode(true));
    saveParticipantButton = document.getElementById('save-participant-button');

    closeParticipantsDetailsButton.removeEventListener(
        'click',
        closeParticipantsDetailsHandler);    
    closeParticipantsDetailsButton.addEventListener(
        'click',
        closeParticipantsDetailsHandler);

    cancelModifyParticipantButton.removeEventListener(
        'click',
        cancelModifyParticipantHandler);    
    cancelModifyParticipantButton.addEventListener(
        'click',
        cancelModifyParticipantHandler);

    startModifyParticipantButton.addEventListener(
        'click',
        startModifyParticipanttHandler.bind(null, participant));

    saveParticipantButton.addEventListener(
        'click',
        saveParticipantHandler.bind(null, participant));
    
};

const toggleBackdrop = () => {
    backdrop.classList.toggle('is-block');
};

const cancelModifyParticipantHandler = () => {
    closeparticipantDetailsModal();

    toggleBackdrop();    
};

const closeParticipantsDetailsHandler = () => {
    closeparticipantDetailsModal();

    toggleBackdrop();
}

const startModifyParticipanttHandler = (person) => {
    const closeParticipantsDetailsButton = document.getElementById('close-participants-details-button');
    const cancelModifyParticipantButton = document.getElementById('cancel-modify-participant-button');
    let startModifyParticipantButton = document.getElementById('start-modify-participant-button');
    let saveParticipantButton = document.getElementById('save-participant-button');

    closeParticipantsDetailsButton.classList.add('is-hidden');
    cancelModifyParticipantButton.classList.remove('is-hidden');
    startModifyParticipantButton.classList.add('is-hidden');
    saveParticipantButton.classList.remove('is-hidden');

    enableParticipantInput();
}

const saveParticipantHandler = (person) => {    
    if(!isPersonFormValid() && !isCompanyFormValid()) {
        return;
    }

    update(person);

    closeparticipantDetailsModal();

    toggleBackdrop();
}

const showDeleteParticipantModalHandler = (id) => {
    deleteParticipantModal.classList.add('is-active');

    toggleBackdrop();

    const cancelDeleteParticipantButton = document.getElementById('cancel-delete-participant-button');
    let deleteParticipantButton = document.getElementById('delete-participant-button');

    deleteParticipantButton.replaceWith(deleteParticipantButton.cloneNode(true));
    deleteParticipantButton = document.getElementById('delete-participant-button');

    cancelDeleteParticipantButton.removeEventListener(
        'click',
        cancelDeleteParticipantHandler);
    
    cancelDeleteParticipantButton.addEventListener(
        'click',
        cancelDeleteParticipantHandler);
    deleteParticipantButton.addEventListener(
        'click',
        deleteParticipantHandler.bind(null, id));
};

const deleteParticipantHandler = (participantId) => {
    let participantIndex = 0;
    console.log(participantId);
    for (const participant of participants) {
        if (participant.id === participantId) {
            break;
        }
        participantIndex++;
    }

    participants.splice(participantIndex, 1);
    const listRoot = document.getElementById('participant-list');
    console.log(listRoot);
    console.log(participantIndex);
    console.log(listRoot.children[participantIndex]);
    listRoot.removeChild(listRoot.children[participantIndex]);

    closeDeleteParticipantModal();

    toggleBackdrop(); 
}

const closeDeleteParticipantModal = () => {
    deleteParticipantModal.classList.remove('is-active');
}

const cancelDeleteParticipantHandler = () => {
    closeParticipantDeleteModal();

    toggleBackdrop();    
};

const closeParticipantDeleteModal = () => {
    participantDeleteModal.classList.remove('is-active');
}

const showPersons = () => {
    toggleEventPersons();
    toggleEventCompanies();
}

const showCompanies = () => {
    toggleEventPersons();
    toggleEventCompanies();
}

const toggleEventPersons = () => {
    personLi.classList.toggle('is-active');
};

const toggleEventCompanies = () => {
    companyLi.classList.toggle('is-active');
};



/*
participantDetailsButton.addEventListener('click', showParticipantDetailsModalHandler);
cancelModifyParticipantButton.addEventListener('click', cancelModifyParticipantHandler);
closeButton.addEventListener('click', cancelModifyParticipantHandler);
startModifyParticipantButton.addEventListener('click', startModifyParticipantHandler);
startDeleteParticipantButton.addEventListener('click', showDeleteParticipantModalHandler);


*/

const load = () => {
    var person1 = new Person(1, 'Liidia', 'Laada', '48708073721', 'Sularaha', 'bla-bla-bla');
    var person2 = new Person(2, 'Sergei', 'Sulima', '222', 'Pangaülekanne', 'bla-bla-bla');
    var person3 = new Person(3, 'Maria', 'Ardel', '333', 'Sularaha', 'bla-bla-bla');

    participants.push(person1);
    participants.push(person2);
    participants.push(person3);

    

    for (const person of participants) {
        console.log(person);
        var personLi = person.render();
        
        const startDeleteParticipantButton = personLi.querySelector('.start-delete-participant-button');
        const startModifyParticipantButton = personLi.querySelector('.participant-details-button');

        console.log(startDeleteParticipantButton);
        console.log(startModifyParticipantButton);
        startDeleteParticipantButton.addEventListener('click', showDeleteParticipantModalHandler.bind(null, person.id));
        startModifyParticipantButton.addEventListener('click', showParticipantDetailsModalHandler.bind(null, person));
   
        participantsList.appendChild(personLi);
    }
}
showPersons();
load();

function closeparticipantDetailsModal() {
    const closeParticipantsDetailsButton = document.getElementById('close-participants-details-button');
    const cancelModifyParticipantButton = document.getElementById('cancel-modify-participant-button');
    let startModifyParticipantButton = document.getElementById('start-modify-participant-button');
    let saveParticipantButton = document.getElementById('save-participant-button');

    closeParticipantsDetailsButton.classList.remove('is-hidden');
    cancelModifyParticipantButton.classList.add('is-hidden');
    startModifyParticipantButton.classList.remove('is-hidden');
    saveParticipantButton.classList.add('is-hidden');
    participantDetailsModal.classList.remove('is-active');
}

