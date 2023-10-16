using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.Xml;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using WpfApp_Project.Models;
using WpfApp_Project.ViewModels.Commands;


namespace WpfApp_Project.ViewModels
{
    public class MainWindowViewModel
    {
        public ObservableCollection<AccountModel> Accounts { get; set; }

        public ICollectionView accountsView { get; set; }

        public AccountModel Account { get; set; }

        public List<string> ComboBoxLookupList { get; set; }

        public string LookupText { get; set; }


        public int number;


        public ICommand AccoutAddCommand { get; set; }
        public ICommand AccoutRemoveCommand { get; set; }
        public ICommand AccoutLookupCommand { get; set; }


        public MainWindowViewModel()
        {
            Account = new AccountModel();
            LookupText = string.Empty;

            InitTestData();
            SetComboBoxData();

            AccoutAddCommand = new AccountCommand(AccountAdd);
            AccoutRemoveCommand = new AccountCommand(AccountRemove, CanAccountRemove);
            AccoutLookupCommand = new AccountCommand(AccountLookup);
        }


        public void AccountAdd(object parameter)
        {
            Task.Run(AccountAddAysc().Wait);
        }

        public async Task AccountAddAysc()
        {
            /* UI에 입력된 정보를 바탕으로 사용자목록(Accounts)에 추가 */

            await Task.Delay(1000);
            AccountModel tempAccountModel = new AccountModel();
            tempAccountModel.Name = Account.Name;
            tempAccountModel.Age = Account.Age;
            tempAccountModel.PhoneNumber = Account.PhoneNumber;

            Accounts.Add(tempAccountModel);
            InitAccoutModel();
        }

        public bool CanAccountRemove(object parameter)
        {
            /* 파라미터에서 listView의 인덱스를 받아오는데,
              10보다 클 경우에만 삭제가 가능한 조건
               (최초 사용자 10인은 삭제가 되면 안됨)*/
            if ((int?)parameter > 9)
            {
                return true;
            }
            return false;
        }

        public void AccountRemove(object parameter)
        {
            Task.Run(AccountRemoveAsync(parameter).Wait);
        }

        public async Task AccountRemoveAsync(object parameter)
        {
            await Task.Delay(1000);
            Accounts.RemoveAt((int)parameter);
        }


        public void AccountLookup(object parameter)
        {
            Task.Run(AccountLookupAsync(parameter).Wait);
        }

        public async Task AccountLookupAsync(object parameter)
        {
            await Task.Delay(1000);

            /* 콤보박스에서 기본값을 이름으로 선택하게끔 설정했으나
               강제로 null값이 들어갈 경우가 있을 경우를 대비해
               null인 경우 바로 반환*/

            string? selected = parameter as string;

            if (selected == null)
            {
                return;
            }

            /* 조회박스에서 입력한 값을 토대로 검색후 출력
               조회값 없이 선택했다면 모든 사용자를 조회함 */

            accountsView = CollectionViewSource.GetDefaultView(Accounts);

            if (selected == "이름")
            {
                accountsView.Filter = o => System.String.IsNullOrEmpty(LookupText) ? true : ((AccountModel)o).Name.Contains(LookupText);
            }
            else if (selected == "연락처")
            {
                accountsView.Filter = o => System.String.IsNullOrEmpty(LookupText) ? true : ((AccountModel)o).PhoneNumber.Contains(LookupText);
            }
        }


        #region /* 임시 데이터 */
        private void InitTestData()
        {
            Accounts = new ObservableCollection<AccountModel>();

            Accounts.Add(new AccountModel() { Name = "손흥민", Age = 20, PhoneNumber = "010-1111-3948" });
            Accounts.Add(new AccountModel() { Name = "이강민", Age = 30, PhoneNumber = "010-2222-1028" });
            Accounts.Add(new AccountModel() { Name = "김민재", Age = 40, PhoneNumber = "010-3333-5093" });
            Accounts.Add(new AccountModel() { Name = "이재성", Age = 50, PhoneNumber = "010-4444-2203" });
            Accounts.Add(new AccountModel() { Name = "백승호", Age = 60, PhoneNumber = "010-5555-0053" });
            Accounts.Add(new AccountModel() { Name = "이승우", Age = 70, PhoneNumber = "010-6666-9995" });
            Accounts.Add(new AccountModel() { Name = "이동국", Age = 80, PhoneNumber = "010-7777-4485" });
            Accounts.Add(new AccountModel() { Name = "박지성", Age = 90, PhoneNumber = "010-8888-7641" });
            Accounts.Add(new AccountModel() { Name = "이청용", Age = 33, PhoneNumber = "010-9999-6642" });
            Accounts.Add(new AccountModel() { Name = "이천수", Age = 23, PhoneNumber = "010-0000-0381" });

        }

        #endregion

        #region /* 콤보박스 데이터 */


        private void SetComboBoxData()
        {
            ComboBoxLookupList = new List<string>()
            {
                "이름",
                "연락처",
            };
        }

        #endregion

        #region /* 데이터 초기화 */
        private void InitAccoutModel()
        {
            Account.Name = string.Empty;
            Account.Age = 0;
            Account.PhoneNumber = string.Empty;
        }


        #endregion

    }
}
