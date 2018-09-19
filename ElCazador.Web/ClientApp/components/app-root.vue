<template>
	<div id="app" class="container-fluid">
		<div class="row">
			<div class="col-sm-2">
				<nav-menu params="route: route"></nav-menu>
			</div>
			<div class="col-sm-10">
				<el-container>
					<el-main>
						<el-row>
							<users></users>
							<targets></targets>
						</el-row>
						<el-row>
							<log></log>
						</el-row>
					</el-main>
				</el-container>
			</div>
		</div>
	</div>
</template>

<script>
import Vue from "vue";

import NavMenu from "./nav-menu";
import Users from "./users";
import Log from "./log";
import Targets from "./targets";

Vue.component("nav-menu", NavMenu);
Vue.component("users", Users);
Vue.component("log", Log);
Vue.component("targets", Targets);

export default {
  data: {
  }
};
</script>

<style>
</style>