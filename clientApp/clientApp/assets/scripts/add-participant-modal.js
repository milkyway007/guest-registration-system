const paymentMethods = ['Pangaülekanne','Sularaha'];

const showPersonForm = () => {
    togglePersonLi();
    toggleCompanyLi();

    togglePersonForm();
    toggleCompanyForm();

    fillPaymentMethodSelect(personPaymentMethodSelect);
}

const showCompanyForm = () => {
    togglePersonLi();
    toggleCompanyLi();

    togglePersonForm();
    toggleCompanyForm();

    fillPaymentMethodSelect(companyPaymentMethodSelect);
}

const togglePersonLi = () => {
    personLi.classList.toggle('is-active');
};

const toggleCompanyLi = () => {
    companyLi.classList.toggle('is-active');
};

const togglePersonForm = () => {
    addPersonForm.classList.toggle('is-hidden');
};

const toggleCompanyForm = () => {
    addCompanyForm.classList.toggle('is-hidden');
}

const fillPaymentMethodSelect = (paymentMethodSelect) => {
    paymentMethods.sort();
    for (var i = 0; i < paymentMethods.length; i++) {
        var option = document.createElement("option");
        option.value = paymentMethods[i];
        option.text = paymentMethods[i];
        paymentMethodSelect.appendChild(option);
    }
}

const showParticipantModalHandler = (eventId) => {
    addParticipantModal.classList.add('is-active');

    toggleBackdrop();

    const cancelAddParticipantButton = document.getElementById('cancel-add-participant-button');
    let addParticipantButton = document.getElementById('add-participant-button');

    addParticipantButton.replaceWith(addParticipantButton.cloneNode(true));
    addParticipantButton = document.getElementById('add-participant-button');

    cancelAddParticipantButton.removeEventListener(
        'click',
        cancelAddParticipantHandler);
    
    cancelAddParticipantButton.addEventListener(
        'click',
        cancelAddParticipantHandler);
    addParticipantButton.addEventListener(
        'click',
        addParticipantHandler.bind(null, eventId));
};

const addParticipantHandler = (eventId) => {
    const isPerson = personLi.classList.contains('is-active');
    if (isPerson) {
        console.log('isPersonFormValid: ' + isPersonFormValid());
        if(!isPersonFormValid()) {
            return;
        }

        const firstName = personFirstNameInput.value;
        const lastName = personLastNameInput.value;
        const code = personCodeInput.value;
        const paymentMethod = personPaymentMethodSelect.value;
        const description = personDescription.value;

        const person = new Person(0, firstName, lastName, code, paymentMethod, description);
        console.log(person);
    }
    else
    {
        if(!isCompanyFormValid()) {
            return;
        }

        const name = companyNameInput.value;
        const code = companyCodeInput.value;
        const paymentMethod = companyPaymentMethodSelect.value;
        const participantsCount = companyParticipantCountInput.value;
        const description = companyDescription.value;

        const company = new Company(0, name, code, participantsCount, paymentMethod, description);
        console.log(company);
    }

    closeParticipantModal();

    toggleBackdrop(); 
}

const isPersonFirstNameValid = () => {
    if (!personFirstNameInput.value) {
        
        personFirstNameInput.classList.add('is-danger');
        personFirstNameValidation.classList.add('is-block');

        return false;
    }

    return true;
}

const isPersonLastNameValid = () => {
    if (!personLastNameInput.value) {
        personLastNameInput.classList.add('is-danger');
        personLastNameValidation.classList.add('is-block');

        return false;
    }

    return true;
}

const isPersonCodeValid = () => {
    if (!personCodeInput.value) {
        personCodeInput.classList.add('is-danger');
        personCodeValidation.classList.add('is-block');

        return false;
    }

    return true;
}

const isCompanyNameValid = () => {
    if (!companyNameInput.value) {
        companyNameInput.classList.add('is-danger');
        companyNameValidation.classList.add('is-block');

        return false;
    }

    return true;
}

const isCompanyCodeValid = () => {
    if (!companyCodeInput.value) {
        companyCodeInput.classList.add('is-danger');
        companyCodeValidation.classList.add('is-block');

        return false;
    }

    return true;
}

const isPersonPaymentMethodValid = () => {
    if(!paymentMethods.includes(personPaymentMethodSelect.value)) {
        personPaymentMethodSelectContainer.classList.add('is-danger');
        personPaymentMethodValidation.classList.add('is-block');

        return false;
    }

    return true;
};

const isCompanyPaymentMethodValid = () => {
    if(!paymentMethods.includes(companyPaymentMethodSelect.value)) {
        companyPaymentMethodSelectContainer.classList.add('is-danger');
        companyPaymentMethodSelect.classList.add('is-block');

        return false;
    }

    return true;
};

const isCompanyParticipantCountValid = () => {
    console.log("Count: " + companyParticipantCountInput.value);
    if(companyParticipantCountInput.value < 1) {
        companyParticipantCountInput.classList.add('is-danger');
        companyParticipantCountValidation.classList.add('is-block');

        return false;
    }

    return true;
};

const isPersonFormValid = () => {
    const isFirstNameValid = isPersonFirstNameValid();
    const isLastNameValid = isPersonLastNameValid();
    const isCodeValid = isPersonCodeValid();
    const isPaymentMethodValid = isPersonPaymentMethodValid();

    return isFirstNameValid && isLastNameValid && isCodeValid && isPaymentMethodValid;
}

const isCompanyFormValid = () => {
    const isNameValid = isCompanyNameValid();
    const isCodeValid = isCompanyCodeValid();
    const isPaymentMethodValid = isCompanyPaymentMethodValid();
    const isParticipantCountValid = isCompanyParticipantCountValid();

    return isNameValid && isCodeValid && isPaymentMethodValid && isParticipantCountValid;
}

const clearPersonValidation = () => {
    personFirstNameInput.classList.remove('is-danger');
    personFirstNameValidation.classList.remove('is-block');
    personLastNameInput.classList.remove('is-danger');
    personLastNameValidation.classList.remove('is-block');
    personCodeInput.classList.remove('is-danger');
    personCodeValidation.classList.remove('is-block');
    personPaymentMethodSelectContainer.classList.remove('is-danger');
    personPaymentMethodValidation.classList.remove('is-block');
}

const clearCompanyValidation = () => {
    companyNameInput.classList.remove('is-danger');
    companyNameValidation.classList.remove('is-block');
    companyCodeInput.classList.remove('is-danger');
    companyCodeValidation.classList.remove('is-block');
    companyParticipantCountInput.classList.remove('is-danger');
    companyParticipantCountValidation.classList.remove('is-block');
    companyPaymentMethodSelectContainer.classList.remove('is-danger');
    companyPaymentMethodValidation.classList.remove('is-block');
}

const clearParticipantInput = () => {
    personFirstNameInput.value='';
    personLastNameInput.value='';
    personCodeInput.value='';
    personPaymentMethodSelectContainer.value='';
    personPaymentMethodSelect.value='';
    personDescription.value='';
    companyNameInput.value='';
    companyCodeInput.value='';
    companyParticipantCountInput.value='';
    companyPaymentMethodSelectContainer.value='';
    companyPaymentMethodSelect.value='';
    companyDescription.value='';
};

const disableParticipantInput = () => {
    personFirstNameInput.disabled = true;
    personLastNameInput.disabled = true;
    personCodeInput.disabled = true;
    personPaymentMethodSelectContainer.disabled = true;
    personPaymentMethodSelect.disabled = true;
    personDescription.disabled = true;
    companyNameInput.disabled = true;
    companyCodeInput.disabled = true;
    companyParticipantCountInput.disabled = true;
    companyPaymentMethodSelectContainer.disabled = true;
    companyPaymentMethodSelect.disabled = true;
    companyDescription.disabled = true;
};

const enableParticipantInput = () => {
    personFirstNameInput.disabled = false;
    personLastNameInput.disabled = false;
    personCodeInput.disabled = false;
    personPaymentMethodSelectContainer.disabled = false;
    personPaymentMethodSelect.disabled = false;
    personDescription.disabled =false;
    companyNameInput.disabled = false;
    companyCodeInput.disabled = false;
    companyParticipantCountInput.disabled = false;
    companyPaymentMethodSelectContainer.disabled = false;
    companyPaymentMethodSelect.disabled = false;
    companyDescription.disabled = false;
};

const initializeFields = (participant) => {
    if(!participant.participantsCount) {
        personFirstNameInput.value = participant.firstName;
        personLastNameInput.value = participant.lastName;
        personCodeInput.value = participant.code;
        personPaymentMethodSelect.value = participant.paymentMethod;
        personDescription.value = participant.description; 

        return;
    }

    companyNameInput.value = participant.name;
    companyCodeInput.value = participant.code;
    companyParticipantCountInput.value = participant.participantsCount;
    companyPaymentMethodSelect.value = participant.paymentMethod;
    companyDescription.value = participant.description;
};

const update = (participant) => {
    if(!personFirstNameInput.value) {
        participant.firstName = personFirstNameInput.value;
        participant.lastName = personLastNameInput.value;
        participant.code  = personCodeInput.value;
        participant.paymentMethod = personPaymentMethodSelect.value;
        participant.description = personDescription.value; 

        return;
    }

    participant.name  = companyNameInput.value;
    participant.code  = companyCodeInput.value;
    participant.participantsCount = companyParticipantCountInput.value;
    participant.paymentMethod = companyPaymentMethodSelect.value;
    participant.description  = companyDescription.value;
};

personFirstNameInput.addEventListener('input', clearPersonValidation);
personLastNameInput.addEventListener('input', clearPersonValidation);
personCodeInput.addEventListener('input', clearPersonValidation);
personPaymentMethodSelect.addEventListener('input', clearPersonValidation);

companyNameInput.addEventListener('input', clearCompanyValidation);
companyCodeInput.addEventListener('input', clearCompanyValidation);
companyParticipantCountInput.addEventListener('input', clearCompanyValidation);
companyPaymentMethodSelect.addEventListener('input', clearCompanyValidation);

showPersonForm();