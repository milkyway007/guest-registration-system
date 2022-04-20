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
    return `${this.line1}, ${this.city}, ${this.country}`;
  }
}

class Event {
  constructor(id, name, occurence, description, address, participants) {
    this.id = id;
    this.name = name;
    this.occurence = occurence;
    this.description = description;
    this.address = address;
    this.participants = participants;
    this.shouldShowButtons = this.occurence > new Date() ? true : false;
  }

  render() {
    const li = document.createElement("li");
    const eventElement = document.createElement("div");
    eventElement.classList.add(
      "event-element",
      "columns",
      "is-flex",
      "is-flex-direction-row",
      "is-justify-content-flex-start"
    );
    li.appendChild(eventElement);
    const image =
      `<div class="event-element_image column is-two-fifths">` +
      `<figure class="image is-16by9">` +
      `<img src="images/event.jpg" alt="${this.name}"/>` +
      `</figure>` +
      `</div>`;
    eventElement.innerHTML = image;
    const eventElementInfo = document.createElement("div");
    eventElementInfo.classList.add(
      "event-element_info",
      "column",
      "is-one-third",
      "is-flex",
      "is-flex-direction-column",
      "is-justify-content-flex-start"
    );
    eventElement.appendChild(eventElementInfo);
    const details =
      `<p class="title is-5 has-text-grey">${this.occurence.toRender()}</p>` +
      `<p class="subtitle is-6 is-spaced has-text-grey">${this.address.toShortString()}</p>` +
      `<a style="min-height:4em;" class="title is-5 is-spaced is-flex is-flex-grow-1 link" href="participants.html?event_id=${this.id}">` +
      `${this.name}</a>` +
      `<p class="has-text-grey-light participant-count">${
        this.participants.length == 1
          ? this.participants.length + " osaleja"
          : this.participants.length + " osalejat"
      }</p>`;
    eventElementInfo.innerHTML = details;
    const eventElementAction = document.createElement("div");
    eventElementAction.classList.add(
      "event-element_action",
      "column",
      "is-one-fifth",
      "is-flex",
      "is-flex-direction-column",
      "is-justify-content-flex-end"
    );
    eventElement.appendChild(eventElementAction);
    const buttons =
      `<div class="is-flex is-flex-direction-row is-justify-content-flex-end">` +
      `<div class="buttons are-small has-addons ${
        !this.shouldShowButtons ? "is-hidden" : ""
      } is-flex is-flex-direction-column">` +
      `<span class="is-flex is-flex-direction-row is-justify-content-flex-start is-align-self-stretch">` +
      `<button class="button is-link start-add-participant-button" title="Lisa osaleja">` +
      `<span class="icon is-small"><i class="fas fa-solid fa-plus"></i></span>` +
      `<span>Lisa osaleja</span>` +
      `</button>` +
      `</span>` +
      `<span class="is-flex is-flex-direction-row is-justify-content-flex-start is-align-self-stretch">` +
      `<button class="button is-danger start-delete-event-button" title="Kustuta üritus">` +
      `<span class="icon is-small"><i class="fas fa-solid fa-xmark"></i></span>` +
      `<span>Kustuta</span>` +
      `</button>` +
      `</span>` +
      `<div>` +
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
  constructor(id, name, code, participantCount, paymentMethod, description) {
    super(id, code, paymentMethod, description);

    this.name = name;
    this.participantCount = participantCount;
  }

  toShortString() {
    return `${this.name} ${this.code}`;
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

export { Address, Event, Participant, Person, Company };
