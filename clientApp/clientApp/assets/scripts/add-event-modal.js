var today = new Date();    
eventDateTimeInput.min = today.toISOString();
const countries = ['Eesti','Läti','Leedu','Soome', 'Rootsi', 'Norra'];

const fillCountrySelectList = () => {
    countries.sort();
    for (var i = 0; i < countries.length; i++) {
        var option = document.createElement("option");
        option.value = countries[i];
        option.text = countries[i];
        addressCountrySelect.appendChild(option);
    }
}

const isEventNameValid = () => {
    if (!eventNameInput.value) {
        eventNameInput.classList.add('is-danger');
        eventNameValidation.classList.add('is-block');

        return false;
    }

    return true;
}

const isEventDateTimeValid = () => {
    const today = new Date();
    if(eventDateTimeInput.value <= today) {
        eventDateTimeInput.classList.add('is-danger');
        eventDateTimeValidation.classList.add('is-block');

        return false;
    }

    return true;
};

const isAddressLine1Valid = () => {
    if (!addressLine1Input.value) {
        addressLine1Input.classList.add('is-danger');
        addressLine1Validation.classList.add('is-block');

        return false;
    }

    return true;
}

const isAddressCityValid = () => {
    if (!addressCityInput.value) {
        addressCityInput.classList.add('is-danger');
        addressCityValidation.classList.add('is-block');

        return false;
    }

    return true;
}

const isAddressCountryValid = () => {
    if(!countries.includes(addressCountrySelect.value)) {
        addressCountrySelectContainer.classList.add('is-danger');
        addressCountryValidation.classList.add('is-block');

        return false;
    }

    return true;
};

const isAddressZipValid = () => {
    if (!addressZipInput.value) {
        addressZipInput.classList.add('is-danger');
        addressZipValidation.classList.add('is-block');

        return false;
    }

    return true;
}

const isEventFormValid = () => {
    const isNameValid = isEventNameValid();
    const isDateTimeValid = isEventDateTimeValid();
    const isLine1Valid = isAddressLine1Valid();
    const isCityValid = isAddressCityValid();
    const isZipValid = isAddressZipValid();
    const isCountryValid = isAddressCountryValid();

    return isNameValid && isDateTimeValid && isLine1Valid && isCityValid && isZipValid && isCountryValid;
}

const addEventHandler = () => {
    const isValid = isEventFormValid();
    if(!isValid) {
        return;
    }

    const name = eventNameInput.value;
    const dateTime = new Date(eventDateTimeInput.value);
    const description = eventDescriptionTextarea.value;

    const line1 = addressLine1Input.value;
    const line2 = addressLine2Input.value;
    const city = addressCityInput.value;
    const state = addressStateInput.value;
    const country = addressCityInput.value;
    const zip = addressZipInput.value;

    const addressId = generateRandomInteger(1000);
    const address = new Address(addressId, line1, line2, city, state, country, zip);

    const eventId = generateRandomInteger(1000);
    const event = new Event(eventId, name, dateTime, description, address);

    closeEventModal();
    clearEventInput();

    toggleBackdrop();

    var eventLi = event.render();
    const startDeleteEventButton = eventLi.querySelector('.start-delete-event-button');
    const startAddParticipantButton = eventLi.querySelector('.start-add-participant-button');
    
    startDeleteEventButton.addEventListener('click', showDeleteEventModalHandler.bind(null, eventId));
    startAddParticipantButton.addEventListener('click', showParticipantModalHandler.bind(null, eventId));

    eventList.appendChild(eventLi);
    events.push(event);
};

const validateAddEventForm = () => {
    var isValidDateTime = validateDateTime();
    var isValidCountrySelect = validateCountrySelect();

    return isValidDateTime && isValidCountrySelect;
}

const generateRandomInteger = (max) => {
    return Math.floor(Math.random() * max) + 1;
}

const clearEventInput = () => {
    eventNameInput.value = '';
    eventDateTimeInput.value = '';
    addressLine2Input.value = '';
    addressLine1Input.value = '';
    addressCityInput.value = '';
    addressStateInput.value = '';
    addressCountrySelectContainer.value = '';
    addressCountrySelect.value = '';
    addressZipInput.value = '';
    eventDescriptionTextarea.value = '';
};

addEventButton.addEventListener('click', addEventHandler);


