import { TranslateService } from '@ngx-translate/core';
import { Component, OnInit, ChangeDetectionStrategy } from '@angular/core';
import { LanguageService } from '@app/core/services/language/language.service';

/**
 * Flags from:
 * https://flagicons.lipis.dev/
 */
type FlagKey = keyof typeof flags;
type Flag = {
  id: number,
  localize: FlagKey,
  template: string,
  selected: boolean,
};

const flags = {
   en: { src: '/assets/images/flags/gb.svg' },
   es: { src: '/assets/images/flags/es.svg' },
};

@Component({
  selector: 'app-language-selector',
  templateUrl: './language-selector.component.html',
  styleUrls: ['./language-selector.component.scss'],
  // changeDetection: ChangeDetectionStrategy.OnPush
})
export class LanguageSelectorComponent implements OnInit {

  isOpen: boolean;
  selectedFlag: Flag;
  flags: Flag[] = [];
  notSelectedFlags: Flag[] = [];
  constructor(
    private translate: TranslateService,
    private languageService: LanguageService,
  ) { }

  ngOnInit(): void {
    this.loadLanguages();
  }

  private async loadLanguages() {
    this.languageService.getLanguages().subscribe(langs => {
      this.flags = langs.map(lang => {
        return {
          id: lang.id,
          localize: lang.localization,
          selected: lang.isActive,
        } as Flag;
      });

      this.selectFlag(this.flags.find(x => x.selected));
      this.updateNotSelectedFlags();
    });
  }

  selectedFlagSrc = () => this.selectedFlag && flags[this.selectedFlag.localize].src;
  flagSrc = (flag: Flag) => flags[flag.localize].src;

  async selectFlag(flag: Flag) {
    this.selectedFlag = flag;
    this.translate.use(this.selectedFlag.localize);
    this.languageService.setLanguage(this.selectedFlag.id);
    this.updateNotSelectedFlags();
  }

  private updateNotSelectedFlags() {
    this.notSelectedFlags = this.flags.filter(flag => flag !== this.selectedFlag);
  }
}
