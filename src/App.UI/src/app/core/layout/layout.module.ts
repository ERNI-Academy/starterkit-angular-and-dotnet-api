import { SharedModule } from '../../shared/shared.module';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { LanguageSelectorComponent } from './language-selector/language-selector.component';
import { UserSelectorComponent } from './user-selector/user-selector.component';
import { NgxPopperModule } from 'ngx-popper';
import { UserLoginComponent } from './user-login/user-login.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterModule,
    SharedModule,
    // It's needed at runtime by the ABB Menu Component (AbbCommonUxMenuModule)
    NgxPopperModule.forRoot({
      // placement: 'bottom-start'
    }),
  ],
  declarations: [LayoutComponent, LanguageSelectorComponent, UserSelectorComponent, UserLoginComponent],
  exports: [LayoutComponent],
})
export class LayoutModule {}
