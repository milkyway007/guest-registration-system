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
      console.log(this.line1)
      console.log(this.city)
      console.log(this.country)
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

  hideButtons (){
    this.placeHolder = 'is-hidden';
  }

  render() {
    const li = document.createElement('li');
    const eventElement = document.createElement('div');
    eventElement.classList.add('event-element', 'is-flex', 'is-flex-direction-row', 'is-justify-content-flex-start');
    li.appendChild(eventElement);
    const image = `<div class="event-element_image">` +
    `<figure class="image is-16by9">` +
    `<img src="images/event.jpg" alt="${this.name}"/>` +
    `</figure>` +
    `</div>`;
    console.log(eventElement);
    eventElement.innerHTML = image;
    const eventElementInfo = document.createElement('div');
    eventElementInfo.classList.add('event-element_info', 'is-flex', 'is-flex-direction-column', 'is-justify-content-flex-start');
    eventElement.appendChild(eventElementInfo);
    const addressShortString = this.address.toShortString();
    const details = `<p class="title is-3 has-text-grey-dark">${this.occurence.toHumanReadableDateTime()}</p>` +
    `<p class="subtitle is-5 is-spaced has-text-grey-dark">${this.address.toShortString()}</p>` +
    `<a class="title is-3 is-spaced is-flex is-flex-grow-1 link" href="participants.html?id=${this.id}">` +
    `${this.name}</a>` +
    `<p class="has-text-grey">${this.participants.length} osalejat</p>`;
    eventElementInfo.innerHTML = details;
    const eventElementAction = document.createElement('div');
    eventElementAction.classList.add('event-element_action', 'is-flex', 'is-flex-direction-column', 'is-justify-content-flex-end');
    eventElement.appendChild(eventElementAction);
    const buttons = `<div class="buttons are-small has-addons ${this.placeholder}">` +
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
    const li = document.createElement('li');
    const participantElement = document.createElement('div');
    li.appendChild(participantElement);
    participantElement.classList.add('participant-element', 'is-flex', 'is-flex-direction-row', 'is-justify-content-start');
    const deleteButtonColumn = document.createElement('div');
    deleteButtonColumn.classList.add('is-flex', 'is-flex-direction--column', 'is-justify-content-center');
    const detailsButtonColumn = deleteButtonColumn.cloneNode(true);
    const participantDataColumn = deleteButtonColumn.cloneNode(true);
    participantElement.appendChild(deleteButtonColumn);
    participantElement.appendChild(detailsButtonColumn);
    participantElement.appendChild(participantDataColumn);
    const deleteButton = `<span>` +
    `<button class="start-delete-participant-button button is-danger is-small" title="Kustuta osaleja">` +
    `<span class="icon">`  +
    `<i class="fas fa-solid fa-xmark"></i>`  +
    `</span>`  +
    `<span>Kustuta</span>`  +
    `</button>`  +
    `</span>`;
    deleteButtonColumn.innerHTML = deleteButton;
    const detailsButton = `<span>`  +
    `<button class="participant-details-button button is-link is-small" title="Vaata osaleja">`  +
    `<span class="icon">`  +
    `<i class="fas fa-solid fa-circle-user"></i>`  +
    `</span>`  +
    `<span>Vaata</span>`  +
    `</button>`  +
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

class Person extends Participant{
  constructor(id, firstName, lastName, code, paymentMethod, description) {
    super(id, code, paymentMethod, description);

    this.firstName = firstName;
    this.lastName = lastName;
  }

  toShortString() {
    return `${this.firstName} ${this.lastName} ${this.code}`;
  }
}
