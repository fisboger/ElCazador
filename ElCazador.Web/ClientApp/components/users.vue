<template>
	<div>
		<el-dialog title="Add user" :visible.sync="addUserVisible" width="20%">
			<el-form :model="form">
				<el-form-item label="Username" :label-width="formLabelWidth">
					<el-input v-model="form.name" autocomplete="off"></el-input>
				</el-form-item>
				<el-form-item label="Password" :label-width="formLabelWidth">
					<el-input v-model="form.name" autocomplete="off"></el-input>
				</el-form-item>
				<el-form-item label="Password type" :label-width="formLabelWidth">
					<el-select v-model="form.region" placeholder="Password type">
						<el-option label="NTLM" value="ntlm"></el-option>
						<el-option label="NetNTLMv2" value="netntlmv2"></el-option>
						<el-option label="Clear-text" value="clearText"></el-option>
					</el-select>
				</el-form-item>
			</el-form>
			<span slot="footer" class="dialog-footer">
				<el-button @click="addUserVisible = false">Cancel</el-button>
				<el-button type="primary" @click="addUserVisible = false">Confirm</el-button>
			</span>
		</el-dialog>

		<el-col :span="12" class="spaced-col">
			<el-card class="box-card">
				<div slot="header" class="clearfix">
					<span>Users</span>
					<el-button style="float: right;" @click="addUserVisible = true" type="primary" icon="el-icon-plus" size="mini" circle></el-button>
				</div>
				<template>
					<el-table v-loading="users.isLoading" :data="users.data" style="min-height:200px" :default-sort="{prop: 'timestamp', order: 'descending'}" max-height="200">
						<!-- <el-table-column sortable prop="timestamp" label="Time">
                  </el-table-column> -->
						<el-table-column sortable prop="username" label="User" min-width="100">
						</el-table-column>
						<el-table-column sortable prop="hash" label="Hash" min-width="300">
						</el-table-column>
						<el-table-column sortable prop="clearTextPw" label="Clear-text" min-width="110">
							<template slot-scope="scope">
								<i v-if="scope.row.clearTextPw" class="el-icon-check"></i>
								<i v-else class="el-icon-close success"></i>
							</template>
						</el-table-column>

						<el-table-column fixed="right" align="right" min-width="40">
							<template slot-scope="scope">
								<el-button class="zero-padding" type="text" icon="el-icon-edit" circle></el-button>
							</template>
						</el-table-column>
					</el-table>
				</template>
			</el-card>
		</el-col>
	</div>
</template>

<script>
export default {
  data() {
    return {
      users: {
        isLoading: false,
        data: [
          {
            timestamp: "2018-05-14 13:00:00",
            username: "sfr",
            machine: "sfr-tp",
            hash: "838ece1033bf7c7468e873e79ba2a3ec",
            clearTextPw: true,
            type: "NTLM",
            "collection-method": "Dump"
          },
          {
            timestamp: "2018-05-14 14:00:00",
            username: "Håndværker",
            machine: "sfr-tp",
            hash: "838ece1033bf7c7468e873e79ba2a3ec",
            clearTextPw: false,
            type: "NTLM",
            "collection-method": "Dump"
          },
          {
            timestamp: "2018-05-14 15:00:00",
            username: "sfr-admin",
            machine: "sfr-tp",
            hash: "838ece1033bf7c7468e873e79ba2a3ec",
            clearTextPw: false,
            type: "NTLM",
            "collection-method": "Dump"
          },
          {
            timestamp: "2018-05-14 16:00:00",
            username: "tap",
            machine: "sfr-tp",
            hash: "838ece1033bf7c7468e873e79ba2a3ec",
            clearTextPw: false,
            type: "NTLM",
            "collection-method": "Dump"
          },
          {
            timestamp: "2018-05-14 17:00:00",
            username: "tju",
            machine: "sfr-tp",
            hash: "838ece1033bf7c7468e873e79ba2a3ec",
            clearTextPw: false,
            type: "NTLM",
            "collection-method": "Dump"
          },
          {
            timestamp: "2018-05-14 18:00:00",
            username: "sfr",
            machine: "sfr-tp",
            hash: "838ece1033bf7c7468e873e79ba2a3ec",
            clearTextPw: false,
            type: "NTLM",
            "collection-method": "Dump"
          },
          {
            timestamp: "2018-05-14 12:00:00",
            username: "sfr",
            machine: "sfr-tp",
            hash: "838ece1033bf7c7468e873e79ba2a3ec",
            clearTextPw: false,
            type: "NTLM",
            "collection-method": "Dump"
          }
        ]
      },
      addUserVisible: false,
      form: {
        username: "",
        password: "",
        passwordType: ""
      },
      formLabelWidth: "120px"
    };
  }
};
</script>

<style>
</style>