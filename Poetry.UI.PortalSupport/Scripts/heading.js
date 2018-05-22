


/* HEADING */

class PortalHeading {
    constructor(text) {
        this.element = document.createElement('h1');
        this.element.className = 'heading';
        this.element.innerText = text;
    }

    appendTo(element) {
        element.appendChild(this.element);

        return this;
    }

    setText(text) {
        this.element.innerText = text;
    }
}