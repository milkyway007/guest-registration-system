import { Event, Address } from "../model.js";
import { postEvent } from "../requests/events/post.js";
import { getEvent, findEventIndex } from "./event_container.js";
import { updateEventParticipantCountAtIndex } from "./event_renderer.js";

const addEventModal = document.getElementById("add-event-modal");
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

const countries = ["Eesti", "Läti", "Leedu", "Soome", "Rootsi", "Norra"];

const updateUiAfterEventUpdate = (eventId, participant) => {
  const eventIndex = findEventIndex(eventId);
  const event = getEvent(eventIndex);

  event.participants.push(participant);

  updateEventParticipantCountAtIndex(eventIndex, event.participants.length);
};

const fillCountrySelectList = () => {
  countries.sort();
  for (var i = 0; i < countries.length; i++) {
    var option = document.createElement("option");
    option.value = countries[i];
    option.text = countries[i];
    addressCountrySelect.appendChild(option);
  }
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
  console.log(eventDateTimeInput.value);
  if (
    !eventDateTimeInput.value ||
    new Date(eventDateTimeInput.value) <= today
  ) {
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

const addEventHandler = () => {
  const isValid = isEventFormValid();
  if (!isValid) {
    return;
  }

  const name = eventNameInput.value;
  const dateTime = new Date(eventDateTimeInput.value);
  console.log(dateTime.toPost());
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
  const participants = [];
  const event = new Event(
    eventId,
    name,
    dateTime,
    description,
    address,
    participants
  );

  postEvent(event);
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

const clearEventInput = () => {
  eventNameInput.value = "";
  eventDateTimeInput.value = "";
  addressLine2Input.value = "";
  addressLine1Input.value = "";
  addressCityInput.value = "";
  addressStateInput.value = "";
  addressCountrySelectContainer.value = "";
  addressCountrySelect.value = "Vali riik";
  addressZipInput.value = "";
  eventDescriptionTextarea.value = "";
};

const closeAddEventModal = () => {
  console.log("addEventModal " + addEventModal);
  if (!addEventModal) {
    return;
  }
  addEventModal.classList.remove("is-active");
  clearEventInput();
  clearEventValidation();
};

const setEventDateTimeInputMin = () => {
  var today = new Date();
  today.setSeconds(0, 0);

  eventDateTimeInput.min = today.toISOString().split(".")[0];
};

const showEventModalHandler = () => {
  setEventDateTimeInputMin();

  addEventModal.classList.add("is-active");
};

export {
  showEventModalHandler,
  closeAddEventModal,
  addEventHandler,
  fillCountrySelectList,
  updateUiAfterEventUpdate,
  clearEventInput,
  clearEventValidation,
};
