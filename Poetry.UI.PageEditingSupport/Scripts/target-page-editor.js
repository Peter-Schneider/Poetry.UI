import WindowMessageManager from './window-message-manager.js';



/* PROPERTY EDITOR TYPES */

var propertyEditorTypes = {};

propertyEditorTypes['string'] = {
    type: 'string',
    createControl: function (get, set) {
        var element = document.createElement('textarea');

        element.classList.add('poetry-page-editing-property-editor');

        var update = () => {
            element.style.height = '';
            element.style.height = element.scrollHeight + 'px';

            set(element.value);
        };

        get()
            .then(value => {
                if (typeof value != 'undefined' && typeof value != 'null') {
                    element.value = value;
                }

                update();
            });

        element.addEventListener('change', update);
        element.addEventListener('keyup', update);

        return element;
    }
};



/* PROPERTY GETTER */

class PropertyGetter {
    constructor() {
        this.messageManager = new WindowMessageManager();
    }

    getValue(name) {
        return new Promise(resolve => {
            var uid = Math.random().toString(36).substr(2);

            var callback = data => {
                if (data.uid != uid) {
                    return;
                }

                resolve(data.value);
                this.messageManager.off('getPropertyValueCallback', callback);
            };

            this.messageManager.on('getPropertyValueCallback', callback);
            this.messageManager.send('getPropertyValue', { uid, name });
        });
    }
}



/* INIT EDITORS */

function initEditors() {
    var messageManager = new WindowMessageManager();
    var propertyGetter = new PropertyGetter();

    [...document.querySelectorAll('.poetry-page-editing-property')].forEach(element => {
        var editorType = propertyEditorTypes[element.getAttribute('field-type')];

        if (!editorType) {
            return;
        }

        var name = element.getAttribute('property-name');

        var sync = throttle(() => messageManager.send('updatePropertyContainers', { properties: [...document.querySelectorAll('.poetry-page-editing-property')].map(getProperty) }));

        var get = () => propertyGetter.getValue(name);
        var set = value => {
            messageManager.send('setPropertyValue', { name, value });
            sync();
        };

        var editor = editorType.createControl(get, set);

        [...element.childNodes].forEach(c => element.removeChild(c));

        element.appendChild(editor);
    });
}

if (document.readyState != 'loading') {
    initEditors();
} else {
    document.addEventListener("DOMContentLoaded", initEditors);
}



/* GET PROPERTY */

function getProperty(element) {
    var elementBoundingClientRect = element.getBoundingClientRect();

    return {
        name: element.getAttribute('property-name'),
        objectId: element.getAttribute('object-id'),
        left: elementBoundingClientRect.left,
        top: elementBoundingClientRect.top,
        width: elementBoundingClientRect.width,
        height: elementBoundingClientRect.height
    };
}



/* HOVER EVENT LISTENER */

function addHoverEventListeners() {
    var messageManager = new WindowMessageManager();

    document.body.addEventListener('mouseover', function (event) {
        var target = event.target;

        while (target && !target.getAttribute('property-name')) {
            target = target.parentElement;
        }

        if (!target) {
            return;
        }

        var name = target.getAttribute('property-name');
        var contentId = target.getAttribute('content-id');

        messageManager.send('mouseOverProperty', { name, contentId });
    });

    document.body.addEventListener('mouseout', function (event) {
        var target = event.target;

        while (target && !target.getAttribute('property-name')) {
            target = target.parentElement;
        }

        if (!target) {
            return;
        }

        var name = target.getAttribute('property-name');
        var contentId = target.getAttribute('content-id');

        messageManager.send('mouseOutProperty', { name, contentId });
    });
};

if (document.readyState != 'loading') {
    addHoverEventListeners();
} else {
    document.addEventListener("DOMContentLoaded", addHoverEventListeners);
}



/* UPDATE PROPERTY CONTAINERS */

function updatePropertyContainers() {
    var messageManager = new WindowMessageManager();

    var update = throttle(() => messageManager.send('updatePropertyContainers', { properties: [...document.querySelectorAll('.poetry-page-editing-property')].map(getProperty) }));

    window.addEventListener('resize', update, true);

    var updateTimeout = () => {
        update();

        setTimeout(updateTimeout, 1000);
    };

    updateTimeout();
}


if (document.readyState != 'loading') {
    updatePropertyContainers();
} else {
    document.addEventListener("DOMContentLoaded", updatePropertyContainers);
}



/* UPDATE DOCUMENT HEIGHT */

function updateDocumentHeight() {
    var messageManager = new WindowMessageManager();

    var update = throttle(() => messageManager.send('updateDocumentHeight', { documentHeight: document.documentElement.scrollHeight }));

    window.addEventListener('resize', update, true);

    var updateTimeout = () => {
        update();

        setTimeout(updateTimeout, 1000);
    };

    updateTimeout();
}

if (document.readyState != 'loading') {
    updateDocumentHeight();
} else {
    document.addEventListener("DOMContentLoaded", updateDocumentHeight);
}



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