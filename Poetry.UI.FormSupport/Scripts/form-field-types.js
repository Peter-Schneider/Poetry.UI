﻿var formFieldTypes = {};

formFieldTypes['string'] = {
    type: 'string',
    createControl: function (field, get, set) {
        var label = document.createElement('label');
        label.classList.add('form-label');

        var labelText = document.createElement('div');
        labelText.classList.add('form-label-text');
        labelText.innerText = field.Name;
        label.appendChild(labelText);

        var input = document.createElement('input');
        input.classList.add('form-input');
        label.appendChild(input);

        input.type = 'text';

        var value = get();

        if (typeof value != 'undefined' && typeof value != 'null') {
            input.value = get();
        }

        function update() {
            set(input.value);
        }

        input.addEventListener('change', update);
        input.addEventListener('keyup', update);

        return label;
    }
};

formFieldTypes['double'] = {
    type: 'double',
    createControl: function (field, get, set) {
        var label = document.createElement('label');
        label.classList.add('form-label');

        var labelText = document.createElement('div');
        labelText.classList.add('form-label-text');
        labelText.innerText = field.Name;
        label.appendChild(labelText);

        var input = document.createElement('input');
        input.classList.add('form-input');
        label.appendChild(input);

        input.type = 'number';
        input.step = 'any';

        var value = get();

        if (typeof value != 'undefined' && typeof value != 'null') {
            input.value = get();
        }

        function update() {
            set(input.value);
        }

        input.addEventListener('change', update);
        input.addEventListener('keyup', update);

        return label;
    }
};

export default formFieldTypes;