


/* CONTEXT MENU */

class ContextMenu {
    constructor() {
        this.element = document.createElement('div');

        this.element.classList.add('poetry-ui-context-menu-outer');

        var button = document.createElement('poetry-ui-context-menu-button');

        button.tabindex = 0;

        this.update = throttle(() => {
            if (!this.element.offsetParent) { // hidden or detached
                return;
            }

            this.menu.classList.remove('poetry-ui-context-menu-right');

            var rectangle = this.menu.getBoundingClientRect();
            var containerRectangle = this.element.offsetParent.getBoundingClientRect();

            if (rectangle.right > containerRectangle.right) {
                this.menu.classList.add('poetry-ui-context-menu-right');
            }
        }, 100);

        button.addEventListener('click', () => {
            button.classList.add('poetry-ui-active');
            this.menu.style.display = '';

            this.update();
        });

        this.documentClickCallback = event => {
            if (event.target == button || event.target == this.menu || event.target.offsetParent == this.menu) {
                return;
            }

            button.classList.remove('poetry-ui-active');
            this.menu.style.display = 'none';
        };

        this.element.appendChild(button);

        this.menu = document.createElement('poetry-ui-context-menu');
        this.menu.style.display = 'none';

        var arrowOuter = document.createElement('div');
        arrowOuter.classList.add('poetry-ui-context-menu-arrow-outer');
        this.menu.appendChild(arrowOuter);

        var arrow1 = document.createElement('div');
        arrow1.classList.add('poetry-ui-context-menu-arrow-1');
        arrowOuter.appendChild(arrow1);

        var arrow2 = document.createElement('div');
        arrow2.classList.add('poetry-ui-context-menu-arrow-2');
        arrowOuter.appendChild(arrow2);

        this.element.appendChild(this.menu);
    }

    addItem(callback) {
        var item = new ContextMenuItem();

        callback(item);

        item.onClick(() => this.menu.style.display = 'none');

        item.appendTo(this.menu);

        return this;
    }

    clearTimer() {
        clearTimeout(this.setTimerReference);
    }

    setTimer() {
        this.update();
        this.setTimerReference = setTimeout(() => this.setTimer(), 1000);
    }

    appendTo(element) {
        element.appendChild(this.element);

        window.addEventListener('resize', this.update);
        document.addEventListener('click', this.documentClickCallback);
        this.setTimer();

        new RemoveElementListener(this.element, () => {
            window.removeEventListener('resize', this.update);
            document.removeEventListener('click', this.documentClickCallback);
            this.clearTimer();
        });
    }
}

class RemoveElementListener {
    constructor(element, callback) {
        var observer = new MutationObserver(mutations => mutations.forEach(mutation => {
            mutation.removedNodes.forEach(node => {
                if (node != element && !node.contains(element)) {
                    return;
                }

                callback();
                observer.disconnect();
            });
        }));

        observer.observe(document.documentElement, {
            childList: true,
            subtree: true,
        });
    }
}

class ContextMenuItem {
    constructor() {
        this.element = document.createElement('poetry-ui-context-menu-item');
    }

    setText(text) {
        this.element.innerText = text;

        return this;
    }

    onClick(callback) {
        this.element.addEventListener('click', callback);

        return this;
    }

    appendTo(element) {
        element.appendChild(this.element);

        return this;
    }
}

export default ContextMenu;



/* THROTTLE */

function throttle(fn, threshhold = 250, scope) {
    var last, deferTimer;
    return function () {
        var context = scope || this;

        var now = +new Date,
            args = arguments;
        if (last && now < last + threshhold) {
            // hold on to it
            clearTimeout(deferTimer);
            deferTimer = setTimeout(function () {
                last = now;
                fn.apply(context, args);
            }, threshhold);
        } else {
            last = now;
            fn.apply(context, args);
        }
    };
}