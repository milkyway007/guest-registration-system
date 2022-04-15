const monthNames = [
  "jaanuar",
  "veebruar",
  "märts",
  "aprill",
  "mai",
  "juuni",
  "juuli",
  "august",
  "september",
  "oktoober",
  "november",
  "detsember",
];

Date.prototype.toHumanReadableDateTime = function () {
  const yyyy = this.getFullYear();
  const mm = this.getMonth(); // getMonth() is zero-based
  const dd = this.getDate();

  const hh = this.getHours();
  const MM = this.getMinutes();

  const result = `${dd}. ${monthNames[mm]} ${yyyy} ${hh}:${MM}`;

  return result;
};

class Address {
  constructor(id, line1, line2, city, state, country, zip) {
    this.id = id;
    this.line1 = line1;
    this.line2 = line2;
    this.city = city;
    this.state = state;
    this.country = country;
    this.zip = zip;
  }

  toShortString() {
    console.log(this.line1);
    console.log(this.city);
    console.log(this.country);
    return `${this.line1}, ${this.city}, ${this.country}`;
  }
}

class Event {
  constructor(id, name, occurence, description, address) {
    this.id = id;
    this.name = name;
    this.occurence = occurence;
    this.description = description;
    this.address = address;
    this.participants = [];
  }

  hideButtons() {
    this.placeHolder = "is-hidden";
  }

  render() {
    const li = document.createElement("li");
    const eventElement = document.createElement("div");
    eventElement.classList.add(
      "event-element",
      "is-flex",
      "is-flex-direction-row",
      "is-justify-content-flex-start"
    );
    li.appendChild(eventElement);
    const image =
      `<div class="event-element_image">` +
      `<figure class="image is-16by9">` +
      `<img src="images/event.jpg" alt="${this.name}"/>` +
      `</figure>` +
      `</div>`;
    console.log(eventElement);
    eventElement.innerHTML = image;
    const eventElementInfo = document.createElement("div");
    eventElementInfo.classList.add(
      "event-element_info",
      "is-flex",
      "is-flex-direction-column",
      "is-justify-content-flex-start"
    );
    eventElement.appendChild(eventElementInfo);
    const addressShortString = this.address.toShortString();
    const details =
      `<p class="title is-3 has-text-grey-dark">${this.occurence.toHumanReadableDateTime()}</p>` +
      `<p class="subtitle is-5 is-spaced has-text-grey-dark">${this.address.toShortString()}</p>` +
      `<a class="title is-3 is-spaced is-flex is-flex-grow-1 link" href="participants.html?id=${this.id}">` +
      `${this.name}</a>` +
      `<p class="has-text-grey">${this.participants.length} osalejat</p>`;
    eventElementInfo.innerHTML = details;
    const eventElementAction = document.createElement("div");
    eventElementAction.classList.add(
      "event-element_action",
      "is-flex",
      "is-flex-direction-column",
      "is-justify-content-flex-end"
    );
    eventElement.appendChild(eventElementAction);
    const buttons =
      `<div class="buttons are-small has-addons ${this.placeholder}">` +
      `<span class="is-flex is-flex-direction-row">` +
      `<button class="button is-link start-add-participant-button" title="Lisa osaleja">` +
      `<span class="icon is-small"><i class="fas fa-solid fa-plus"></i></span>` +
      `<span>Lisa osaleja</span>` +
      `</button>` +
      `</span>` +
      `<span>` +
      `<button class="button is-danger start-delete-event-button" title="Kustuta üritus">` +
      `<span class="icon is-small"><i class="fas fa-solid fa-xmark"></i></span>` +
      `<span>Kustuta</span>` +
      `</button>` +
      `</span>` +
      `<div>`;
    eventElementAction.innerHTML = buttons;

    return li;
  }
}

class Participant {
  constructor(id, code, paymentMethod, description) {
    this.id = id;
    this.code = code;
    this.paymentMethod = paymentMethod;
    this.description = description;
  }

  toShortString() {}
  render() {
    const li = document.createElement("li");
    const participantElement = document.createElement("div");
    li.appendChild(participantElement);
    participantElement.classList.add(
      "participant-element",
      "is-flex",
      "is-flex-direction-row",
      "is-justify-content-start"
    );
    const deleteButtonColumn = document.createElement("div");
    deleteButtonColumn.classList.add(
      "is-flex",
      "is-flex-direction--column",
      "is-justify-content-center"
    );
    const detailsButtonColumn = deleteButtonColumn.cloneNode(true);
    const participantDataColumn = deleteButtonColumn.cloneNode(true);
    participantElement.appendChild(deleteButtonColumn);
    participantElement.appendChild(detailsButtonColumn);
    participantElement.appendChild(participantDataColumn);
    const deleteButton =
      `<span>` +
      `<button class="start-delete-participant-button button is-danger is-small" title="Kustuta osaleja">` +
      `<span class="icon">` +
      `<i class="fas fa-solid fa-xmark"></i>` +
      `</span>` +
      `<span>Kustuta</span>` +
      `</button>` +
      `</span>`;
    deleteButtonColumn.innerHTML = deleteButton;
    const detailsButton =
      `<span>` +
      `<button class="participant-details-button button is-link is-small" title="Vaata osaleja">` +
      `<span class="icon">` +
      `<i class="fas fa-solid fa-circle-user"></i>` +
      `</span>` +
      `<span>Vaata</span>` +
      `</button>` +
      `</span>`;
    detailsButtonColumn.innerHTML = detailsButton;
    const participantDetails = `<span>${this.toShortString()}</span>`;
    participantDataColumn.innerHTML = participantDetails;

    return li;
  }
}

class Company extends Participant {
  constructor(id, name, code, participantsCount, paymentMethod, description) {
    super(id, code, paymentMethod, description);

    this.name = name;
    this.participantsCount = participantsCount;
  }

  toShortString() {
    return `${this.Name} ${this.code}`;
  }
}

class Person extends Participant {
  constructor(id, firstName, lastName, code, paymentMethod, description) {
    super(id, code, paymentMethod, description);

    this.firstName = firstName;
    this.lastName = lastName;
  }

  toShortString() {
    return `${this.firstName} ${this.lastName} ${this.code}`;
  }
}

const startAddEventButton = document.getElementById("start-add-event-button");
const addEventModal = document.getElementById("add-event-modal");
const cancelAddEventButton = document.getElementById("cancel-add-event-button");
const addEventButton = document.getElementById("add-event-button");
const eventDateTimeInput = document.getElementById("event-datetime-input");
const eventNameInput = document.getElementById("event-name-input");
const addressLine2Input = document.getElementById("address-line-2-input");
const addressLine1Input = document.getElementById("address-line-1-input");
const addressCityInput = document.getElementById("address-city-input");
const addressStateInput = document.getElementById("address-state-input");
const addressCountrySelectContainer = document.getElementById(
  "address-country-select-container"
);
const addressCountrySelect = document.getElementById("address-country-select");
const addressZipInput = document.getElementById("address-zip-input");
const eventDescriptionTextarea = document.getElementById(
  "event-description-textarea"
);
const eventNameValidation = document.getElementById("event-name-validation");
const eventDateTimeValidation = document.getElementById(
  "event-datetime-validation"
);
const addressLine1Validation = document.getElementById(
  "address-line-1-validation"
);
const addressCityValidation = document.getElementById(
  "address-city-validation"
);
const addressCountryValidation = document.getElementById(
  "address-country-validation"
);
const addressZipValidation = document.getElementById("address-zip-validation");
const eventUlElemet = document.getElementById("event-list");
const deleteEventModal = document.getElementById("delete-event-modal");
const futureEventsLi = document.getElementById("future-events-li");
const pastEventsLi = document.getElementById("past-events-li");
const addParticipantModal = document.getElementById(
  "participant-details-modal"
);
var personLiElement = document.getElementById("person-li");
var companyLiElement = document.getElementById("company-li");
var addPersonForm = document.getElementById("person-form");
var addCompanyForm = document.getElementById("company-form");
const companyPaymentMethodSelectContainer = document.getElementById(
  "company-payment-method-select-container"
);
const companyPaymentMethodSelect = document.getElementById(
  "company-payment-method-select"
);
const personPaymentMethodSelectContainer = document.getElementById(
  "person-payment-method-select-container"
);
const personPaymentMethodSelect = document.getElementById(
  "person-payment-method-select"
);

const personFirstNameInput = document.getElementById("person-firstname-input");
const personLastNameInput = document.getElementById("person-lastname-input");
const personCodeInput = document.getElementById("person-code-input");
const personDescription = document.getElementById("person-description");
const companyNameInput = document.getElementById("company-name-input");
const companyCodeInput = document.getElementById("company-code-input");
const companyParticipantCountInput = document.getElementById(
  "company-participant-count-input"
);
const companyDescription = document.getElementById("company-description");

const personFirstNameValidation = document.getElementById(
  "person-firstname-validation"
);
const personLastNameValidation = document.getElementById(
  "person-lastname-validation"
);
const personCodeValidation = document.getElementById("person-code-validation");
const personPaymentMethodValidation = document.getElementById(
  "person-payment-method-validation"
);
const companyNameValidation = document.getElementById(
  "company-name-validation"
);
const companyCodeValidation = document.getElementById(
  "company-code-validation"
);
const companyPaymentMethodValidation = document.getElementById(
  "company-payment-method-validation"
);
const companyParticipantCountValidation = document.getElementById(
  "company-participant-count-validation"
);
const paymentMethods = ["Pangaülekanne", "Sularaha"];
const countries = ["Eesti", "Läti", "Leedu", "Soome", "Rootsi", "Norra"];
const events = [];

function removeOptions(selectElement) {
  var i,
    L = selectElement.options.length - 2;
  for (i = L; i >= 0; i--) {
    selectElement.remove(i);
  }
}

const fillCountrySelectList = () => {
  removeOptions(addressCountrySelect);
  countries.sort();
  for (var i = 0; i < countries.length; i++) {
    var option = document.createElement("option");
    option.value = countries[i];
    option.text = countries[i];
    addressCountrySelect.appendChild(option);
  }
};

const toggleBackdrop = () => {
  console.log("toggle");
  backdrop.classList.toggle("is-block");
};

const showEventModalHandler = () => {
  fillCountrySelectList();
  addEventModal.classList.add("is-active");

  toggleBackdrop();
};

const closeEventModal = () => {
  addEventModal.classList.remove("is-active");
};

const clearEventInput = () => {
  eventNameInput.value = "";
  eventDateTimeInput.value = "";
  addressLine2Input.value = "";
  addressLine1Input.value = "";
  addressCityInput.value = "";
  addressStateInput.value = "";
  addressCountrySelectContainer.value = "";
  addressCountrySelect.value = "";
  addressZipInput.value = "";
  eventDescriptionTextarea.value = "";
};

const clearEventValidation = () => {
  eventNameInput.classList.remove("is-danger");
  eventNameValidation.classList.remove("is-block");
  eventDateTimeInput.classList.remove("is-danger");
  eventDateTimeValidation.classList.remove("is-block");
  addressLine1Input.classList.remove("is-danger");
  addressLine1Validation.classList.remove("is-block");
  addressCityInput.classList.remove("is-danger");
  addressCityValidation.classList.remove("is-block");
  addressCountrySelectContainer.classList.remove("is-danger");
  addressCountryValidation.classList.remove("is-block");
  addressZipInput.classList.remove("is-danger");
  addressZipValidation.classList.remove("is-block");
};

const cancelAddEventHandler = () => {
  closeEventModal();
  clearEventInput();
  clearEventValidation();

  toggleBackdrop();
};

const isEventNameValid = () => {
  if (!eventNameInput.value) {
    eventNameInput.classList.add("is-danger");
    eventNameValidation.classList.add("is-block");

    return false;
  }

  return true;
};

const isEventDateTimeValid = () => {
  const today = new Date();
  if (eventDateTimeInput.value <= today) {
    eventDateTimeInput.classList.add("is-danger");
    eventDateTimeValidation.classList.add("is-block");

    return false;
  }

  return true;
};

const isAddressLine1Valid = () => {
  if (!addressLine1Input.value) {
    addressLine1Input.classList.add("is-danger");
    addressLine1Validation.classList.add("is-block");

    return false;
  }

  return true;
};

const isAddressCityValid = () => {
  if (!addressCityInput.value) {
    addressCityInput.classList.add("is-danger");
    addressCityValidation.classList.add("is-block");

    return false;
  }

  return true;
};

const isAddressCountryValid = () => {
  if (!countries.includes(addressCountrySelect.value)) {
    addressCountrySelectContainer.classList.add("is-danger");
    addressCountryValidation.classList.add("is-block");

    return false;
  }

  return true;
};

const isAddressZipValid = () => {
  if (!addressZipInput.value) {
    addressZipInput.classList.add("is-danger");
    addressZipValidation.classList.add("is-block");

    return false;
  }

  return true;
};

const isEventFormValid = () => {
  const isNameValid = isEventNameValid();
  const isDateTimeValid = isEventDateTimeValid();
  const isLine1Valid = isAddressLine1Valid();
  const isCityValid = isAddressCityValid();
  const isZipValid = isAddressZipValid();
  const isCountryValid = isAddressCountryValid();

  return (
    isNameValid &&
    isDateTimeValid &&
    isLine1Valid &&
    isCityValid &&
    isZipValid &&
    isCountryValid
  );
};

const closeDeleteEventModal = () => {
  deleteEventModal.classList.remove("is-active");
};

const cancelDeleteEventModalHandler = () => {
  closeDeleteEventModal();

  toggleBackdrop();
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
  const listRoot = document.getElementById("event-list");
  listRoot.removeChild(listRoot.children[eventIndex]);

  closeDeleteEventModal();

  toggleBackdrop();
};

const showDeleteEventModalHandler = (id) => {
  deleteEventModal.classList.add("is-active");

  toggleBackdrop();

  const cancelDeleteEventButton = document.getElementById(
    "cancel-delete-event-button"
  );
  let deleteEventButton = document.getElementById("delete-event-button");

  deleteEventButton.replaceWith(deleteEventButton.cloneNode(true));
  deleteEventButton = document.getElementById("delete-event-button");

  cancelDeleteEventButton.removeEventListener(
    "click",
    cancelDeleteEventModalHandler
  );

  cancelDeleteEventButton.addEventListener(
    "click",
    cancelDeleteEventModalHandler
  );
  deleteEventButton.addEventListener(
    "click",
    deleteEventHandler.bind(null, id)
  );
};

const addParticipantHandler = (eventId) => {
  const isPerson = personLiElement.classList.contains("is-active");
  if (isPerson) {
    console.log("isPersonFormValid: " + isPersonFormValid());
    if (!isPersonFormValid()) {
      return;
    }

    const firstName = personFirstNameInput.value;
    const lastName = personLastNameInput.value;
    const code = personCodeInput.value;
    const paymentMethod = personPaymentMethodSelect.value;
    const description = personDescription.value;

    const person = new Person(
      0,
      firstName,
      lastName,
      code,
      paymentMethod,
      description
    );
    console.log(person);
  } else {
    if (!isCompanyFormValid()) {
      return;
    }

    const name = companyNameInput.value;
    const code = companyCodeInput.value;
    const paymentMethod = companyPaymentMethodSelect.value;
    const participantsCount = companyParticipantCountInput.value;
    const description = companyDescription.value;

    const company = new Company(
      0,
      name,
      code,
      participantsCount,
      paymentMethod,
      description
    );
    console.log(company);
  }

  closeParticipantModal();

  toggleBackdrop();
};

const cancelAddParticipantHandler = () => {
  closeParticipantModal();
  clearParticipantInput();

  toggleBackdrop();
};

const showParticipantModalHandler = (eventId) => {
  addParticipantModal.classList.add("is-active");

  toggleBackdrop();

  const cancelAddParticipantButton = document.getElementById(
    "cancel-add-participant-button"
  );
  let addParticipantButton = document.getElementById("add-participant-button");

  addParticipantButton.replaceWith(addParticipantButton.cloneNode(true));
  addParticipantButton = document.getElementById("add-participant-button");

  cancelAddParticipantButton.removeEventListener(
    "click",
    cancelAddParticipantHandler
  );

  cancelAddParticipantButton.addEventListener(
    "click",
    cancelAddParticipantHandler
  );
  addParticipantButton.addEventListener(
    "click",
    addParticipantHandler.bind(null, eventId)
  );
};

const addEventHandler = () => {
  const isValid = isEventFormValid();
  if (!isValid) {
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

  const addressId = 0;
  const address = new Address(
    addressId,
    line1,
    line2,
    city,
    state,
    country,
    zip
  );

  const eventId = 0;
  const event = new Event(eventId, name, dateTime, description, address);

  closeEventModal();
  clearEventInput();

  toggleBackdrop();

  var eventLiElement = event.render();
  const startDeleteEventButton = eventLiElement.querySelector(
    ".start-delete-event-button"
  );
  const startAddParticipantButton = eventLiElement.querySelector(
    ".start-add-participant-button"
  );

  startDeleteEventButton.addEventListener(
    "click",
    showDeleteEventModalHandler.bind(null, eventId)
  );
  startAddParticipantButton.addEventListener(
    "click",
    showParticipantModalHandler.bind(null, eventId)
  );

  eventUlElemet.appendChild(eventLiElement);
  events.push(event);
};

const showFutureEvents = () => {
  futureEventsLi.classList.toggle("is-active");
  pastEventsLi.classList.toggle("is-active");
};

const showPastEvents = () => {
  futureEventsLi.classList.toggle("is-active");
  pastEventsLi.classList.toggle("is-active");
};

const fillPaymentMethodSelect = (paymentMethodSelect) => {
  removeOptions(paymentMethodSelect);
  paymentMethods.sort();
  paymentMethods;
  for (var i = 0; i < paymentMethods.length; i++) {
    var option = document.createElement("option");
    option.value = paymentMethods[i];
    option.text = paymentMethods[i];
    paymentMethodSelect.appendChild(option);
  }
};

const showPersonForm = () => {
  personLiElement.classList.toggle("is-active");
  companyLiElement.classList.toggle("is-active");

  addPersonForm.classList.toggle("is-hidden");
  addCompanyForm.classList.toggle("is-hidden");

  fillPaymentMethodSelect(personPaymentMethodSelect);
};

const showCompanyForm = () => {
  personLiElement.classList.toggle("is-active");
  companyLiElement.classList.toggle("is-active");

  addPersonForm.classList.toggle("is-hidden");
  addCompanyForm.classList.toggle("is-hidden");

  fillPaymentMethodSelect(companyPaymentMethodSelect);
};

const isPersonFirstNameValid = () => {
  if (!personFirstNameInput.value) {
    personFirstNameInput.classList.add("is-danger");
    personFirstNameValidation.classList.add("is-block");

    return false;
  }

  return true;
};

const isPersonLastNameValid = () => {
  if (!personLastNameInput.value) {
    personLastNameInput.classList.add("is-danger");
    personLastNameValidation.classList.add("is-block");

    return false;
  }

  return true;
};

const isPersonCodeValid = () => {
  if (!personCodeInput.value) {
    personCodeInput.classList.add("is-danger");
    personCodeValidation.classList.add("is-block");

    return false;
  }

  return true;
};

const isCompanyNameValid = () => {
  if (!companyNameInput.value) {
    companyNameInput.classList.add("is-danger");
    companyNameValidation.classList.add("is-block");

    return false;
  }

  return true;
};

const isCompanyCodeValid = () => {
  if (!companyCodeInput.value) {
    companyCodeInput.classList.add("is-danger");
    companyCodeValidation.classList.add("is-block");

    return false;
  }

  return true;
};

const isPersonPaymentMethodValid = () => {
  if (!paymentMethods.includes(personPaymentMethodSelect.value)) {
    personPaymentMethodSelectContainer.classList.add("is-danger");
    personPaymentMethodValidation.classList.add("is-block");

    return false;
  }

  return true;
};

const isCompanyPaymentMethodValid = () => {
  if (!paymentMethods.includes(companyPaymentMethodSelect.value)) {
    companyPaymentMethodSelectContainer.classList.add("is-danger");
    companyPaymentMethodSelect.classList.add("is-block");

    return false;
  }

  return true;
};

const isCompanyParticipantCountValid = () => {
  console.log("Count: " + companyParticipantCountInput.value);
  if (companyParticipantCountInput.value < 1) {
    companyParticipantCountInput.classList.add("is-danger");
    companyParticipantCountValidation.classList.add("is-block");

    return false;
  }

  return true;
};

const isPersonFormValid = () => {
  const isFirstNameValid = isPersonFirstNameValid();
  const isLastNameValid = isPersonLastNameValid();
  const isCodeValid = isPersonCodeValid();
  const isPaymentMethodValid = isPersonPaymentMethodValid();

  return (
    isFirstNameValid && isLastNameValid && isCodeValid && isPaymentMethodValid
  );
};

const isCompanyFormValid = () => {
  const isNameValid = isCompanyNameValid();
  const isCodeValid = isCompanyCodeValid();
  const isPaymentMethodValid = isCompanyPaymentMethodValid();
  const isParticipantCountValid = isCompanyParticipantCountValid();

  return (
    isNameValid &&
    isCodeValid &&
    isPaymentMethodValid &&
    isParticipantCountValid
  );
};

const clearPersonValidation = () => {
  personFirstNameInput.classList.remove("is-danger");
  personFirstNameValidation.classList.remove("is-block");
  personLastNameInput.classList.remove("is-danger");
  personLastNameValidation.classList.remove("is-block");
  personCodeInput.classList.remove("is-danger");
  personCodeValidation.classList.remove("is-block");
  personPaymentMethodSelectContainer.classList.remove("is-danger");
  personPaymentMethodValidation.classList.remove("is-block");
};

const clearCompanyValidation = () => {
  companyNameInput.classList.remove("is-danger");
  companyNameValidation.classList.remove("is-block");
  companyCodeInput.classList.remove("is-danger");
  companyCodeValidation.classList.remove("is-block");
  companyParticipantCountInput.classList.remove("is-danger");
  companyParticipantCountValidation.classList.remove("is-block");
  companyPaymentMethodSelectContainer.classList.remove("is-danger");
  companyPaymentMethodValidation.classList.remove("is-block");
};

const clearParticipantInput = () => {
  personFirstNameInput.value = "";
  personLastNameInput.value = "";
  personCodeInput.value = "";
  personPaymentMethodSelectContainer.value = "";
  personPaymentMethodSelect.value = "";
  personDescription.value = "";
  companyNameInput.value = "";
  companyCodeInput.value = "";
  companyParticipantCountInput.value = "";
  companyPaymentMethodSelectContainer.value = "";
  companyPaymentMethodSelect.value = "";
  companyDescription.value = "";
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
  personDescription.disabled = false;
  companyNameInput.disabled = false;
  companyCodeInput.disabled = false;
  companyParticipantCountInput.disabled = false;
  companyPaymentMethodSelectContainer.disabled = false;
  companyPaymentMethodSelect.disabled = false;
  companyDescription.disabled = false;
};

const update = (participant) => {
  if (!personFirstNameInput.value) {
    participant.firstName = personFirstNameInput.value;
    participant.lastName = personLastNameInput.value;
    participant.code = personCodeInput.value;
    participant.paymentMethod = personPaymentMethodSelect.value;
    participant.description = personDescription.value;

    return;
  }

  participant.name = companyNameInput.value;
  participant.code = companyCodeInput.value;
  participant.participantsCount = companyParticipantCountInput.value;
  participant.paymentMethod = companyPaymentMethodSelect.value;
  participant.description = companyDescription.value;
};

const closeParticipantModal = () => {
  addParticipantModal.classList.remove("is-active");
};

startAddEventButton.addEventListener("click", showEventModalHandler);
cancelAddEventButton.addEventListener("click", cancelAddEventHandler);
addEventButton.addEventListener("click", addEventHandler);

personFirstNameInput.addEventListener("input", clearPersonValidation);
personLastNameInput.addEventListener("input", clearPersonValidation);
personCodeInput.addEventListener("input", clearPersonValidation);
personPaymentMethodSelect.addEventListener("input", clearPersonValidation);

companyNameInput.addEventListener("input", clearCompanyValidation);
companyCodeInput.addEventListener("input", clearCompanyValidation);
companyParticipantCountInput.addEventListener("input", clearCompanyValidation);
companyPaymentMethodSelect.addEventListener("input", clearCompanyValidation);
/*
const participantDetailsModal = document.getElementById(
  "participant-details-modal"
);

var addPersonForm = document.getElementById("person-form");
var addCompanyForm = document.getElementById("company-form");

var Element = document.getElementById("person-li");
var Element = document.getElementById("company-li");

const personFirstNameInput = document.getElementById("person-firstname-input");
const personLastNameInput = document.getElementById("person-lastname-input");
const personCodeInput = document.getElementById("person-code-input");
const personPaymentMethodSelectContainer = document.getElementById(
  "person-payment-method-select-container"
);
const personPaymentMethodSelect = document.getElementById(
  "person-payment-method-select"
);
const personDescription = document.getElementById("person-description");
const companyNameInput = document.getElementById("company-name-input");
const companyCodeInput = document.getElementById("company-code-input");
const companyParticipantCountInput = document.getElementById(
  "company-participant-count-input"
);
const companyPaymentMethodSelectContainer = document.getElementById(
  "company-payment-method-select-container"
);
const companyPaymentMethodSelect = document.getElementById(
  "company-payment-method-select"
);
const companyDescription = document.getElementById("company-description");

const personFirstNameValidation = document.getElementById(
  "person-firstname-validation"
);
const personLastNameValidation = document.getElementById(
  "person-lastname-validation"
);
const personCodeValidation = document.getElementById("person-code-validation");
const personPaymentMethodValidation = document.getElementById(
  "person-payment-method-validation"
);
const companyNameValidation = document.getElementById(
  "company-name-validation"
);
const companyCodeValidation = document.getElementById(
  "company-code-validation"
);
const companyPaymentMethodValidation = document.getElementById(
  "company-payment-method-validation"
);
const companyParticipantCountValidation = document.getElementById(
  "company-participant-count-validation"
);

const backdrop = document.getElementById("backdrop");

const userEventInputs = addEventModal.querySelectorAll("input");

const addParticipantModal = document.getElementById("add-participant-modal");





const buttons = document.getElementsByClassName(".event-element_action");
const participantDetailsButton = document.getElementById(
  "participant-details-button"
);
//const participantDetailsModal = document.getElementById('participant-details-modal');

const closeButton = document.getElementById(
  "close-participants-details-button"
);
const cancelModifyParticipantButton = document.getElementById(
  "cancel-modify-participant-button"
);
const startModifyParticipantButton = document.getElementById(
  "start-modify-participant-button"
);

const cancelDeleteParticipantButton = document.getElementById(
  "cancel-delete-participant-button"
);
const participantDeleteModal = document.getElementById(
  "delete-participant-modal"
);

const userParticipantInputs = participantDetailsModal.querySelectorAll("input");
const participantSelects = participantDetailsModal.querySelectorAll("select");
const participantTextareas =
  participantDetailsModal.querySelectorAll("textarea");

const startDeleteParticipantButton = document.getElementById(
  "start-delete-participant-button"
);
const deleteParticipantModal = document.getElementById(
  "delete-participant-modal"
);
const cancelDeleteEventButton = document.getElementById(
  "cancel-delete-participant-button"
);

var Element = document.getElementById("person-li");
var Element = document.getElementById("company-li");

var participantsList = document.getElementById("participant-list");














showPersonForm();

const validateAddEventForm = () => {
  var isValidDateTime = validateDateTime();
  var isValidCountrySelect = validateCountrySelect();

  return isValidDateTime && isValidCountrySelect;
};

const generateRandomInteger = (max) => {
  return Math.floor(Math.random() * max) + 1;
};





const cancelDeleteEventModalHandler = () => {
  closeDeleteEventModal();

  toggleBackdrop();
};

const closeParticipantModal = () => {
  addParticipantModal.classList.remove("is-active");
};



const showFutureEvents = () => {
  toggleFutureEventsLi();
  togglePastEventsLi();
};

const showPastEvents = () => {
  toggleFutureEventsLi();
  togglePastEventsLi();
};

const toggleFutureEventsLi = () => {
  futureEventsLi.classList.toggle("is-active");
};

const togglePastEventsLi = () => {
  pastEventsLi.classList.toggle("is-active");
};

const loadEvents = () => {
  for (const event of events) {
    eventUlElemet.appendChild(event.render());
  }
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
  const listRoot = document.getElementById("event-list");
  listRoot.removeChild(listRoot.children[eventIndex]);

  closeDeleteEventModal();

  toggleBackdrop();
};

const backdropClickHandler = () => {
  console.log("here");
  closeEventModal();
  closeParticipantModal();

  clearEventInput();
  clearParticipantInput();

  closeDeleteEventModal();

  toggleBackdrop();
};

backdrop.addEventListener("click", backdropClickHandler);


showFutureEvents();

const participants = [];

const showParticipantDetailsModalHandler = (participant) => {
  initializeFields(participant);
  disableParticipantInput();

  participantDetailsModal.classList.add("is-active");

  toggleBackdrop();

  const closeParticipantsDetailsButton = document.getElementById(
    "close-participants-details-button"
  );
  const cancelModifyParticipantButton = document.getElementById(
    "cancel-modify-participant-button"
  );
  let startModifyParticipantButton = document.getElementById(
    "start-modify-participant-button"
  );
  let saveParticipantButton = document.getElementById(
    "save-participant-button"
  );

  startModifyParticipantButton.replaceWith(
    startModifyParticipantButton.cloneNode(true)
  );
  startModifyParticipantButton = document.getElementById(
    "start-modify-participant-button"
  );

  saveParticipantButton.replaceWith(saveParticipantButton.cloneNode(true));
  saveParticipantButton = document.getElementById("save-participant-button");

  closeParticipantsDetailsButton.removeEventListener(
    "click",
    closeParticipantsDetailsHandler
  );
  closeParticipantsDetailsButton.addEventListener(
    "click",
    closeParticipantsDetailsHandler
  );

  cancelModifyParticipantButton.removeEventListener(
    "click",
    cancelModifyParticipantHandler
  );
  cancelModifyParticipantButton.addEventListener(
    "click",
    cancelModifyParticipantHandler
  );

  startModifyParticipantButton.addEventListener(
    "click",
    startModifyParticipanttHandler.bind(null, participant)
  );

  saveParticipantButton.addEventListener(
    "click",
    saveParticipantHandler.bind(null, participant)
  );
};

const cancelModifyParticipantHandler = () => {
  closeparticipantDetailsModal();

  toggleBackdrop();
};

const closeParticipantsDetailsHandler = () => {
  closeparticipantDetailsModal();

  toggleBackdrop();
};

const startModifyParticipanttHandler = (person) => {
  const closeParticipantsDetailsButton = document.getElementById(
    "close-participants-details-button"
  );
  const cancelModifyParticipantButton = document.getElementById(
    "cancel-modify-participant-button"
  );
  let startModifyParticipantButton = document.getElementById(
    "start-modify-participant-button"
  );
  let saveParticipantButton = document.getElementById(
    "save-participant-button"
  );

  closeParticipantsDetailsButton.classList.add("is-hidden");
  cancelModifyParticipantButton.classList.remove("is-hidden");
  startModifyParticipantButton.classList.add("is-hidden");
  saveParticipantButton.classList.remove("is-hidden");

  enableParticipantInput();
};

const saveParticipantHandler = (person) => {
  if (!isPersonFormValid() && !isCompanyFormValid()) {
    return;
  }

  update(person);

  closeparticipantDetailsModal();

  toggleBackdrop();
};

const showDeleteParticipantModalHandler = (id) => {
  deleteParticipantModal.classList.add("is-active");

  toggleBackdrop();

  const cancelDeleteParticipantButton = document.getElementById(
    "cancel-delete-participant-button"
  );
  let deleteParticipantButton = document.getElementById(
    "delete-participant-button"
  );

  deleteParticipantButton.replaceWith(deleteParticipantButton.cloneNode(true));
  deleteParticipantButton = document.getElementById(
    "delete-participant-button"
  );

  cancelDeleteParticipantButton.removeEventListener(
    "click",
    cancelDeleteParticipantHandler
  );

  cancelDeleteParticipantButton.addEventListener(
    "click",
    cancelDeleteParticipantHandler
  );
  deleteParticipantButton.addEventListener(
    "click",
    deleteParticipantHandler.bind(null, id)
  );
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
  const listRoot = document.getElementById("participant-list");
  console.log(listRoot);
  console.log(participantIndex);
  console.log(listRoot.children[participantIndex]);
  listRoot.removeChild(listRoot.children[participantIndex]);

  closeDeleteParticipantModal();

  toggleBackdrop();
};

const closeDeleteParticipantModal = () => {
  deleteParticipantModal.classList.remove("is-active");
};

const cancelDeleteParticipantHandler = () => {
  closeParticipantDeleteModal();

  toggleBackdrop();
};

const closeParticipantDeleteModal = () => {
  participantDeleteModal.classList.remove("is-active");
};

const showPersons = () => {
  toggleEventPersons();
  toggleEventCompanies();
};

const showCompanies = () => {
  toggleEventPersons();
  toggleEventCompanies();
};

const toggleEventPersons = () => {
  Element.classList.toggle("is-active");
};

const toggleEventCompanies = () => {
  Element.classList.toggle("is-active");
};

/*
const load = () => {
  var person1 = new Person(
    1,
    "Liidia",
    "Laada",
    "48708073721",
    "Sularaha",
    "bla-bla-bla"
  );
  var person2 = new Person(
    2,
    "Sergei",
    "Sulima",
    "222",
    "Pangaülekanne",
    "bla-bla-bla"
  );
  var person3 = new Person(
    3,
    "Maria",
    "Ardel",
    "333",
    "Sularaha",
    "bla-bla-bla"
  );

  participants.push(person1);
  participants.push(person2);
  participants.push(person3);

  for (const person of participants) {
    console.log(person);
    var Element = person.render();

    const startDeleteParticipantButton = Element.querySelector(
      ".start-delete-participant-button"
    );
    const startModifyParticipantButton = Element.querySelector(
      ".participant-details-button"
    );

    console.log(startDeleteParticipantButton);
    console.log(startModifyParticipantButton);
    startDeleteParticipantButton.addEventListener(
      "click",
      showDeleteParticipantModalHandler.bind(null, person.id)
    );
    startModifyParticipantButton.addEventListener(
      "click",
      showParticipantDetailsModalHandler.bind(null, person)
    );

    participantsList.appendChild(Element);
  }
};
load();

showPersons();

function closeparticipantDetailsModal() {
  const closeParticipantsDetailsButton = document.getElementById(
    "close-participants-details-button"
  );
  const cancelModifyParticipantButton = document.getElementById(
    "cancel-modify-participant-button"
  );
  let startModifyParticipantButton = document.getElementById(
    "start-modify-participant-button"
  );
  let saveParticipantButton = document.getElementById(
    "save-participant-button"
  );

  closeParticipantsDetailsButton.classList.remove("is-hidden");
  cancelModifyParticipantButton.classList.add("is-hidden");
  startModifyParticipantButton.classList.remove("is-hidden");
  saveParticipantButton.classList.add("is-hidden");
  participantDetailsModal.classList.remove("is-active");

}
*/
