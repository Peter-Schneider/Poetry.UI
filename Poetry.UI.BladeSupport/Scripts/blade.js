


/* BLADE */

class Blade {
    constructor() {
        this.element = document.createElement('poetry-ui-blade-outer');
        this.element.setAttribute('tabindex', '0');
        this.inner = document.createElement('poetry-ui-blade');
        this.element.appendChild(this.inner);
        this.containers = {};
    }

    setFullscreen() {
        this.element.classList.add('poetry-ui-blade-fullscreen');
    }

    setItems(id, items) {
        if (!this.containers[id]) {
            this.containers[id] = document.createElement(id);
            this.inner.appendChild(this.containers[id]);
        } else {
            [...this.containers[id].children].forEach(this.containers[id].removeChild);
        }
        
        if (items.length) {
            this.containers[id].style.display = '';
        } else {
            this.containers[id].style.display = 'none';
        }

        items.forEach(item => {
            if (item instanceof Node) {
                this.containers[id].appendChild(item);
            } else if (item.element instanceof Node) {
                this.containers[id].appendChild(item.element);
            } else {
                var text = document.createElement(`${id}-text`);
                text.innerText = item;
                this.containers[id].appendChild(text);
            }
        });
    }

    setTitle(...items) {
        this.setItems('poetry-ui-blade-title', items);
    }

    setToolbar(...items) {
        this.setItems('poetry-ui-blade-toolbar', items);
    }

    setContent(...items) {
        this.setItems('poetry-ui-blade-content', items);
    }

    setCustomContent(...items) {
        this.setItems('poetry-ui-blade-custom-content', items);
    }

    onClose(callback) {
        this.onCloseCallback = callback;

        return this;
    }

    triggerOnClose(...parameters) {
        if (!this.onCloseCallback) {
            return;
        }

        this.onCloseCallback(...parameters);
    }
}

export default Blade;