import { PanoramaTemplatePage } from './app.po';

describe('Panorama App', function() {
  let page: PanoramaTemplatePage;

  beforeEach(() => {
    page = new PanoramaTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
