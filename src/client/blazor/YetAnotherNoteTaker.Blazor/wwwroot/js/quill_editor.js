window.startQuill = (body) => {
    try {
        var quill = new Quill('#editor-container', {
            modules: {
                formula: true,
                syntax: true,
                toolbar: '#toolbar-container'
            },
            placeholder: '...',
            theme: 'snow'
        });
    } catch (e) {
    }

    try {
        document.getElementById('btnTodo').onclick = () => {
            quill.insertText(quill.getLength() - 1, '☐');
            quill.setSelection(quill.getLength() - 1, 0);
        }
    } catch (e) {
    }

    try {
        document.querySelector('.ql-editor').innerHTML = body;
    } catch (e) {
    }
}

window.getQuillContent = () => document.querySelector('.ql-editor').innerHTML