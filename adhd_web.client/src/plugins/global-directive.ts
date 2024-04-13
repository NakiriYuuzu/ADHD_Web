import SliderTab from '../directives/SliderTab';

export default {
  install: (app) => {
    app.directive('slider-tab', SliderTab)
  }
}