const questionType = {
    text: 0,
    radio: 1,
    checkbox: 2,
    file: 3,
    rate: 4,
    range: 5
}

$(`#add-question`).on(`click`, () => {
    debugger;
    $(`#questions-container`).append(`
        <div class="question-card d-flex flex-column a p-5 mt-4">
              <div class="question">
                <div class="text-center">
                  <input type="text" placeholder="Question" class="question-title input_form w-50 mt-3">
                </div>
                <div class="w-100 mt-3" id="choose-type">
                  <input name="question-type" class="survey-field" type="radio" id="one-answer">
                  <label for="one-answer">One answer</label>
                  <input name="question-type" class="survey-field" type="radio" id="some-answers">
                  <label for="some-answers">Some answers</label>
                  <input name="question-type" class="survey-field" type="radio" id="text-question">
                  <label for="text-question">Text question</label>
                  <input name="question-type" class="survey-field" type="radio" id="file-question">
                  <label for="file-question">File question</label>   
                  <input name="question-type" class="survey-field" type="radio" id="rate-question">
                  <label for="file-question">Rating question</label> 
                  <input name="question-type" class="survey-field" type="radio" id="scale-question">
                  <label for="file-question">Range question</label> 
                </div>
              <div class="question-body mt-3"></div>
              <div id="controll" class="mt-3">
                <input type="button" class="delete-question survey-field w-25" value="Delete question">
            </div>
              </div>
          </div>
    `);

    $(`input[name=question-type][type=radio]`).change((event) => {
        let layout = ``;
        switch ($(event.target).attr(`id`)) {
            case `one-answer`:
                layout += `
                        <div class="options text-center">
                            <input type="text" class="survey-field input_form answer-option mt-2 w-75" 
                            placeholder="Answer option">
                        </div>
                        <div id="controll" class="mt-3">
                            <input type="hidden" class="type" value="radio">
                            <input type="button" class="action-button survey-control survey-field mt-4 w-25"
                                id="add-option" value="Add options">
                        </div>
                    `;
                break;
            case `some-answers`:
                layout += `
                         <div class="options text-center">
                            <input type="text" class="survey-field input_form answer-option mt-2 w-75" 
                            placeholder="Answer option">
                        </div>
                         
                        <div id="controll" class="mt-3">
                            <input type="hidden" class="type" value="checkbox">
                            <input type="button" class="action-button survey-control survey-field mt-4 w-25"
                                id="add-option" value="Add options">
                        </div>
                    `;
                break;
            case `text-question`:
                layout += `
                    <input type="hidden" class="type" value="text">
                `;
                break;
            case `file-question`:
                layout += `
                    <input type="hidden" class="type" value="file">`;
                break;
            case `rate-question`:
                layout += `
                    <input type="hidden" class="type" value="rate">`;
                break;
            case `scale-question`:
                layout += `
                    <input type="hidden" class="type" value="range">`;
                break;
            default:
                alert(`No such question type`);
                return;
        }

        $(event.target).parent().parent().find('.question-body').html(`.question-body`).html(layout);
    });
});

$(document).on(`click`, `#add-option`, (event) => {
    $(event.target).parent().parent().find(`.options`).append(`<input type="text" 
    class="survey-field input_form answer-option mt-2 w-75" placeholder="Answer option">`);
});

$(document).on(`click`, `.delete-question`, function () {
    $(this).closest(`.question-card`).remove();
});