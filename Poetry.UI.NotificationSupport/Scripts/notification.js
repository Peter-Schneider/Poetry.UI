import FadeOutEffect from '../../Portal/Scripts/Effects/fade-out.js';
import FadeInEffect from '../../Portal/Scripts/Effects/fade-in.js';



/* NOTIFICATION */

class Notification {
    constructor() {
        this.element = document.createElement('poetry-ui-notification');
        this.element.setAttribute('tabindex', 0);
        this.element.addEventListener('click', event => {
            if (event.target.matches('poetry-ui-portal-button')) {
                return;
            }

            this.close();
        });
        this.element.addEventListener('keyup', event => {
            if (event.keyCode != 13) {
                return;
            }

            event.preventDefault();
            this.element.click();
        });

        this.closeButton = document.createElement('poetry-ui-notification-close');
        this.closeButton.setAttribute('tabindex', 0);
        this.closeButton.addEventListener('click', event => {
            event.stopPropagation();

            this.close();
        });
        this.closeButton.addEventListener('keyup', event => {
            if (event.keyCode != 13) {
                return;
            }

            event.preventDefault();
            this.closeButton.click();
        });
        this.element.appendChild(this.closeButton);
        this.content = document.createElement('poetry-ui-notification-content');
        this.element.appendChild(this.content);
        this.footer = document.createElement('poetry-ui-notification-footer');
        this.element.appendChild(this.footer);
        this.buttons = document.createElement('poetry-ui-notification-buttons');
        this.footer.appendChild(this.buttons);
        this.source = document.createElement('poetry-ui-notification-source');
        this.source.setAttribute('tabindex', 0);
        this.footer.appendChild(this.source);

        new FadeInEffect(this.element);
    }

    close() {
        new FadeOutEffect(this.element).onComplete(() => this.element.parentElement.removeChild(this.element));

        return this;
    }

    onClick(callback) {
        this.element.addEventListener('click', callback);

        return this;
    }

    setContent(...items) {
        setContents(this.content, items);

        return this;
    }

    setSource(...items) {
        setContents(this.source, items);

        return this;
    }

    setButtons(...items) {
        setContents(this.buttons, items);

        return this;
    }
}

export default Notification;

function setContents(container, items) {
    items.forEach(item => {
        if (item instanceof Node) {
            container.appendChild(item);
        } else if (item.element instanceof Node) {
            container.appendChild(item.element);
        } else {
            container.innerText = item;
        }
    });
}